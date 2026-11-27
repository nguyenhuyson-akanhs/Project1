using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Sciptforpickable : MonoBehaviour
{
    [SerializeField] private PickableScriptObject _PickableScriptObject;
    public GameObject _InventoryContainer;
    public GameObject _PrefabsForIconInInventory;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("okk");
        if (collision.gameObject.CompareTag("Player")) OnPickUp();
    }
    private void OnPickUp()
    {
        _PickableScriptObject.PickUp(this.gameObject,
            gameObject.GetComponent<SpriteRenderer>().sprite,
            _InventoryContainer, _PrefabsForIconInInventory);
    }
}
