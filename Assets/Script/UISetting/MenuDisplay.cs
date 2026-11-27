using UnityEngine;  
using UnityEngine.UI;

public class MenuDisplay : MonoBehaviour
{
    [Header("Tabs")]
    public Image[] _TabsIm;
    public Image[] _PagesIm;
    private void OnEnable()
    {
        Display(0);
    }
    public void Display(int _index)
    {
        foreach (var tab in _TabsIm)
        {
            tab.color = Color.gray;
        }

        foreach (var page in _PagesIm) { 
            page.gameObject.SetActive(false);
        }

        _TabsIm[_index].color = Color.white;
        _PagesIm[_index].gameObject.SetActive(true);
    }
}
