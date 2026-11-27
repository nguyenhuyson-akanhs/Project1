using System;
using UnityEngine;

public class Gems : MonoBehaviour, IItems
{
    public static event Action<int> OnGemsCollect;
    public int GemValue = 5;

    // Lưu lại điểm spawn mà item này đang đứng
    [HideInInspector] public Transform currentSpawnPoint;

    public void Effect()
    {
        //Debug.Log("Boom");
    }

    public void Collect()
    {
        OnGemsCollect?.Invoke(GemValue);

        // TRƯỚC KHI ẨN: Giải phóng điểm spawn cũ (nếu có)
        if (currentSpawnPoint != null)
        {
            // Đổi tên hoặc xóa tag tùy bạn, ở đây ta sẽ dùng logic của SpawnManager để check trạng thái dựa trên tên hoặc component.
            // Nhưng cách sạch nhất là báo cho SpawnManager hoặc tự xóa liên kết.
            currentSpawnPoint = null;
        }

        gameObject.SetActive(false);
    }
}