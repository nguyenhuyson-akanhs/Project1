using UnityEngine;

public class Test : MonoBehaviour
{
    public ItemSO ItemSO1;
    public ItemSO ItemSO2;
    public InventoryManager inventoryManager;

    public virtual void OnInteract()
    {
        for (int i = 0; i < 20; i++)
        {
            ItemInstance instance1 = new(ItemSO1);
            ItemInstance instance2 = new(ItemSO2);

            inventoryManager.AddItem(instance1);
            inventoryManager.AddItem(instance2);
        }
        //ItemInstance instance1 = new(ItemSO1);
        //ItemInstance instance2 = new(ItemSO2);

        //inventoryManager.AddItem(instance1);
        //inventoryManager.AddItem(instance2);
    }
}
