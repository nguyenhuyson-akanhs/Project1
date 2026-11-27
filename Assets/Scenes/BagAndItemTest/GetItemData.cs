using System.Collections.Generic;
using UnityEngine;

public class GetItemData : ScriptableObject
{
    public List<ItemSO> ListItem;
    public Dictionary<string, ItemSO> ItemData;

    protected virtual void Intitialize()
    {
        if (ItemData != null) return;
        ItemData = new();
        foreach(var item in ListItem)
        {
            if (!item) Debug.Log("item here null");
            else
            {
                ItemData.Add(item._NameID, item);
            }
        }
    }

    protected virtual ItemSO GetItemByID(string NameID)
    {
        if (ItemData == null) Intitialize();
        if(ItemData.TryGetValue(NameID, out var item)) return item;

        Debug.Log("Item Not Found");
        return null;
    }
}
