using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public List<ItemSO> _ListItem;

    public bool _Trigger = false;
    DroppedItem dropped;

    private void Update()
    {
        if (_Trigger)
        {
            // call death;

            _Trigger = false;
        }
    }

    protected virtual void OnDeath()
    {
        int index = UnityEngine.Random.Range(0, _ListItem.Count);
        
        dropped.Drop(_ListItem[index]);
    }
}
