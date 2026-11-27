using UnityEngine;
public abstract class ItemScriptObj : ScriptableObject
{
    [Header("Identity & Visuals")]
    public int _ItemID;
    public string _ItemName;
    public string _ItemDescription;
    public Sprite _Icon;
    public GameObject _Prefab;

    [Header("Classification & Attributes")]
    public ItemType _ItemType;
    public Rarity _Rarity;
    public bool Stackable;
    public int StackSize;

    [Header("Economy")]
    public bool _Sellable;
    public int _BuyPrice;
    public int _SellPrice;

    public abstract void Use();

    public abstract string DisplayInfo();

    public abstract void PickUp(GameObject _GameObject, Sprite _sprite, GameObject _InventoryContainer, GameObject _PrefabsForIconInInventory);
}
public enum ItemType { Consumable, Weapon, Armor, QuestItem }
public enum Rarity { Common, Uncommon, Rare, Epic, Legendary }
