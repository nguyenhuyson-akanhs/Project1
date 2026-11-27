using UnityEngine;

public class HeavyCalculation : MonoBehaviour
{
    void Start()
    {
        // Chạy một phép toán tốn CPU ngay khi vừa load
        for (int i = 0; i < 50000000; i++)
        {
           float e = Mathf.Sqrt(i) * Mathf.Sin(i);
        }
    }
}
