using UnityEngine;

public class NewItemData : MonoBehaviour
{
    protected ItemInstance _ItemInstance; 

    public void SetUpData(ItemInstance itemInstance)
    {
        _ItemInstance = itemInstance;
    }
}
