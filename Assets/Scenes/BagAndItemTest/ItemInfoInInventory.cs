using TMPro;
using UnityEngine;

public class ItemInfoInInventory : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI _Text;
    [HideInInspector] public int _IndexInPool { get; private set; }
    [HideInInspector] public string _ItemNameID { get; private set; }

    protected virtual void Awake()
    {
        _Text = GetComponentInChildren<TextMeshProUGUI>();
    }

    protected virtual void Start()
    {
        _IndexInPool = 0;
        _ItemNameID = string.Empty;
    }
    public virtual void ReadItemInstance(in ItemInstance Item)
    {
        _Text.text = $"{Item._NameID} stack: {Item._CurrentQuantity}";
        _IndexInPool = Item._IndexInPool;
        _ItemNameID = Item._NameID;
    }
}
