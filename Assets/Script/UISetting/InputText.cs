using UnityEngine;
using TMPro;

public class InputText : MonoBehaviour
{
    public TMP_InputField tmp_InputField;
    [HideInInspector] public string _StringText;
    public void ReadStringInput()
    {
        _StringText = tmp_InputField.text;
    }
    public void WriteString()
    {
        tmp_InputField.text = _StringText;
    }
}
