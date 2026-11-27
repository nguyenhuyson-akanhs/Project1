using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] protected GridLayoutGroup _GridGroup;
    [SerializeField] protected AssetReferenceGameObject _SlotPrefabAsset;
    protected AsyncOperationHandle<GameObject> _HandleLinkReference;
    protected GameObject _GameObjectHandle;
    [SerializeField] protected GameObject _SlotsContainer;
    [SerializeField] protected List<GameObject> _ListSlot = new();
    protected SortedDictionary<string, ItemInstance> _ID_Item = new();
    protected Queue<ItemInstance> _ListItemQueue = new();

    protected virtual async Awaitable Start()
    {
        _GridGroup.enabled = false;

        _IsLoading = true;

        _HandleLinkReference = Addressables.LoadAssetAsync<GameObject>(_SlotPrefabAsset);
        _GameObjectHandle = await _HandleLinkReference.Task;

        _IsLoading = false;
    }
    public virtual void AddItem(ItemInstance Item)
    {
        _ListItemQueue.Enqueue(Item);

        if (!_ExecutingAddItem)
        {
            _ExecutingAddItem = true;
            _ = AddItemFromQueue();
        }
    }

    protected bool _ExecutingAddItem = false;
    protected virtual async Awaitable AddItemFromQueue()
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        while (_ListItemQueue.Count != 0)
        {
            ItemInstance Item = _ListItemQueue.Dequeue();

            if (_ID_Item.ContainsKey(Item._NameID))
            {
                Item = _ID_Item[Item._NameID];
                Item._CurrentQuantity++;
                _ID_Item[Item._NameID] = Item;

                int index = Item._IndexInPool;
                GameObject obj = _ListSlot[index];
                if (obj.TryGetComponent<ItemInfoInInventory>(out var itemInfo))
                {
                    itemInfo.ReadItemInstance(Item);
                }
            }

            else
            {
                Item._CurrentQuantity++;
                _ID_Item.Add(Item._NameID, Item);
                await Awaitable.NextFrameAsync();

                await CreateNewSlot();
            }
        }

        _ExecutingAddItem = false;

        stopwatch.Stop();
        UnityEngine.Debug.Log($"Thời gian thực thi: {stopwatch.ElapsedMilliseconds} ms");
    }

    protected bool _IsLoading = false;
    protected virtual async Awaitable CreateNewSlot()
    {
        while (_IsLoading) await Awaitable.NextFrameAsync();
        _GridGroup.enabled = true;

        while (_ID_Item.Count != _ListSlot.Count)
        {
            GameObject newSlot = Instantiate(_GameObjectHandle, _SlotsContainer.transform, false);
            _ListSlot.Add(newSlot);
        }

        await Awaitable.NextFrameAsync();
        _GridGroup.enabled = false;
        UpdateUI();
    }

    protected virtual void UpdateUI()
    {
        int index = 0;
        List<string> keys = new List<string>(_ID_Item.Keys);

        foreach (var key in keys)
        {
            ItemInstance i = _ID_Item[key];
            i._IndexInPool = index;
            _ID_Item[key] = i;

            GameObject obj = _ListSlot[index];
            if (obj.TryGetComponent<ItemInfoInInventory>(out var info))
            {
                if (i._NameID != info._ItemNameID)
                {

                    info.ReadItemInstance(i);
                }
            }

            index++;
        }
    }

    private void OnDestroy()
    {
        if (_HandleLinkReference.IsValid())
        {
            Addressables.Release(_HandleLinkReference);
        }
    }
}
