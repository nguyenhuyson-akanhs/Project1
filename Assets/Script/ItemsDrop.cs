using UnityEngine;

[System.Serializable]
public class ItemsDrop
{
    public GameObject ItemPrefabs;
    [Range(0, 100)] public float DropChance;
}
