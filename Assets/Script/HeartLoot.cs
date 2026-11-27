using System;
using UnityEngine;

public class HeartLoot : MonoBehaviour, IItems
{
    public int HealValue = 1;
    public static event Action<int> HeartCollected;
    public void Effect()
    {

    }
    public void Collect()
    {
        HeartCollected?.Invoke(HealValue);
        gameObject.SetActive(false);
    }
}
