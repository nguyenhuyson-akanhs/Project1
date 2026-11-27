using UnityEngine;

public class DroppedItem : MonoBehaviour
{
    public ItemInstance _ItemInstance;

    public virtual void Drop(ItemSO itemSO)
    {
        GameObject newItem = Instantiate(itemSO._Prefab);
        if (newItem.TryGetComponent<NewItemData>(out var component)) component.SetUpData(new ItemInstance(itemSO));
    }
}
