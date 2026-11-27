using System;
using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]
public class InvItems : MonoBehaviour
{
    [SerializeField] private ItemScriptObj _ItemScriptObj;
    private SpriteRenderer _SpriteRenderer;
    public static event Action<string> ShowInfo;
    public static event Action ConcealInfo;
    private void Awake()
    {
        _SpriteRenderer = GetComponent<SpriteRenderer>();
        _SpriteRenderer.sprite = _ItemScriptObj._Icon;
    }
    public void OnHover()
    {
        ShowInfo?.Invoke(_ItemScriptObj.DisplayInfo());
    }
    public void CancelHover()
    {
        ConcealInfo?.Invoke();
    }
    public void OnSelect()
    {
        _ItemScriptObj.Use();
    }
}
