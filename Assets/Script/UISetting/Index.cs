using Unity.VisualScripting;
using UnityEngine;

public class Index : MonoBehaviour, Tabs
{
    [SerializeField] private int _index = -1;
    public int TabsIndex => _index;
    private void OnValidate()
    {
        if (_index < 0) Debug.LogWarning("Chỉnh index đi cu");
    }
} 
