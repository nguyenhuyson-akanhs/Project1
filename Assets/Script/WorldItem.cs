using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class WorldItem : MonoBehaviour
{
    [SerializeField] private ItemScriptObj _ItemScriptObj;
    private BoxCollider2D _BoxCollider2D;
    private SpriteRenderer _SpriteRenderer;
    private void Start()
    {
        _BoxCollider2D = _ItemScriptObj._Prefab.GetComponent<BoxCollider2D>();
        _SpriteRenderer = _ItemScriptObj._Prefab.GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log($"Đã nhận được vật phẩm {_ItemScriptObj._ItemName}");
            Destroy(gameObject);
        }
    }
}
