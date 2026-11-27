using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ConsumableScriptObj", menuName = "ScriptableObjects/ConsumableScriptObj")]
public class ConsumableScriptObj : ItemScriptObj
{
    [Header("ConsumableType")]
    public ConsumableType _ConsumableType;
    public int _Value;

    public override void Use()
    {
        if (_ConsumableType == ConsumableType.ExpPotion) Debug.Log("Tăng " + _Value + " Exp");
        if (_ConsumableType == ConsumableType.HpPotion) Debug.Log("Tăng " + _Value + " Hp");
        if (_ConsumableType == ConsumableType.ManaPotion) Debug.Log("Tăng " + _Value + " Mana");
    }
    public override string DisplayInfo()
    {
        return $"Vật phẩm: {base._ItemName}{Environment.NewLine}Value: {_Value}{Environment.NewLine}{_ItemDescription}";
    }
    public override void PickUp(GameObject _GameObject, Sprite _sprite, GameObject _InventoryContainer, GameObject _PrefabsForIconInInventory)
    {

    }
}
public enum ConsumableType { HpPotion, ManaPotion, ExpPotion }
