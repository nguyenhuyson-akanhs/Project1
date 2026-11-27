using TMPro;
using UnityEngine;

public class ShowItemInfoWhenHover : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _TextMeshPro;
    private void OnEnable()
    {
        InvItems.ShowInfo += ShowInfo;
        InvItems.ConcealInfo += ConcealInfo;
    }
    private void Start()
    {
        _TextMeshPro.text = "";
    }
    private void ShowInfo(string _text)
    {
        _TextMeshPro.text = _text;
    }
    private void ConcealInfo()
    {
        _TextMeshPro.text = "";
    }
    private void OnDisable()
    {
        InvItems.ShowInfo -= ShowInfo;
        InvItems.ConcealInfo -= ConcealInfo;
    }

}
