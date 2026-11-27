using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO",menuName ="ScriptableObjects/ItemSO")]
public class ItemSO : ScriptableObject
{
    [Header("Static Info")]
    public string _NameID;

    [Header("Model")]
    public GameObject _Prefab;
    
}
