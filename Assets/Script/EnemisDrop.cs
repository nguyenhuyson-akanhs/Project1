using System.Collections.Generic;
using UnityEngine;

public class EnemisDrop : MonoBehaviour
{
    [Header("LootList")]
    public List<ItemsDrop> ListDrop = new();

    public void Drop()
    {
        foreach(var item in ListDrop)
        {
            float roll = UnityEngine.Random.Range(0f, 100f);
            if(roll <= item.DropChance)
            {
                GameObject obj = Instantiate(item.ItemPrefabs, gameObject.transform.position, Quaternion.identity);
                break;
            }
        }
    }
}

