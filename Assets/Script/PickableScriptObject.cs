using UnityEngine;
using System;
using Unity.VisualScripting;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "PickableScriptObject", menuName = "ScriptableObjects/PickableScriptObject")]
public class PickableScriptObject : ItemScriptObj
{
    public override string DisplayInfo()
    {
        return $"Vật phẩm: {base._ItemName}{Environment.NewLine}{Environment.NewLine}{_ItemDescription}";
    }

    public override void PickUp(GameObject _GameObject,Sprite _sprite, GameObject _InventoryContainer, GameObject _PrefabsForIconInInventory)
    {
        GameObject Object = Instantiate(_PrefabsForIconInInventory);
        Object.transform.SetParent(_InventoryContainer.transform, false);
        Object.GetComponent<Image>().sprite = _sprite;

        Destroy(_GameObject);
    }

    public override void Use()
    {

    }
}
