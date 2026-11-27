using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Image HeartPrefabs;
    public Sprite FullHeart;
    public Sprite EmptyHeart;
    private List<Image> Hearts = new List<Image>();
    public void SetMaxHearts(int maxHearts)
    {
        foreach (Image heart in Hearts) Destroy(heart.gameObject);
        Hearts.Clear();
        for (int i = 0; i < maxHearts; i++)
        {
            Image newHeart = Instantiate(HeartPrefabs, transform);
            newHeart.sprite = FullHeart;
            Hearts.Add(newHeart);
        }
    }
    public void UpdateHeart(int CurrentHearts)
    {
        for (int i = 0; i < Hearts.Count; i++)
        {
            if (i < CurrentHearts)
            {
                Hearts[i].sprite = FullHeart;
            }
            else Hearts[i].sprite = EmptyHeart;
        }
    }
}
