using System.Collections.Generic;
using UnityEngine;

public class GhostSpawner : MonoBehaviour
{
    [Header("Ghost Settings")]
    [SerializeField] private GameObject ghostPrefab; // Prefab chứa SpriteRenderer và GhostEffect
    [SerializeField] private float ghostDelay = 0.04f; // Khoảng thời gian giữa các bóng ma
    [SerializeField] private Color ghostColor = Color.white; // Màu của bóng ma (nếu muốn đổi màu)

    [Header("Pool Settings")]
    [SerializeField] private int poolSize = 20; // Số lượng bóng ma khởi tạo sẵn
    private List<GameObject> ghostPool = new List<GameObject>();

    private SpriteRenderer characterSR;
    private float ghostDelayTimer;

    // Biến kiểm soát: Chỉ bật khi nhân vật đang Dash hoặc di chuyển nhanh
    public bool isMakingGhost = false;

    private void Start()
    {
        characterSR = GetComponent<SpriteRenderer>();
        InitPool();
    }

    // Khởi tạo Object Pool
    private void InitPool()
    {
        GameObject PoolContainer = new GameObject("GhostContanier");

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(ghostPrefab,PoolContainer.transform.position,Quaternion.identity);
            obj.SetActive(false); // Ẩn đi lúc đầu
            ghostPool.Add(obj);
        }
    }

    // Hàm lấy một GameObject đang rảnh trong Pool
    private GameObject GetPooledGhost()
    {
        for (int i = 0; i < ghostPool.Count; i++)
        {
            if (!ghostPool[i].activeInHierarchy)
            {
                return ghostPool[i];
            }
        }

        // Nếu Pool bị thiếu (hiếm khi xảy ra nếu đặt poolSize đủ lớn), tự động tạo thêm
        GameObject obj = Instantiate(ghostPrefab);
        obj.SetActive(false);
        ghostPool.Add(obj);
        return obj;
    }

    private void Update()
    {
        if (isMakingGhost)
        {
            ghostDelayTimer -= Time.deltaTime;
            if (ghostDelayTimer <= 0)
            {
                SpawnGhost();
                ghostDelayTimer = ghostDelay; // Reset bộ đếm thời gian
            }
        }
    }

    private void SpawnGhost()
    {
        GameObject ghost = GetPooledGhost();
        if (ghost != null)
        {
            GhostEffect ghostScript = ghost.GetComponent<GhostEffect>();

            // "Chụp" các thông số hiện tại của Sprite nhân vật và truyền vào bóng ma
            ghostScript.ActivateGhost(
                characterSR.sprite,
                transform.position,
                transform.rotation,
                transform.localScale,
                ghostColor
            );
        }
    }
}