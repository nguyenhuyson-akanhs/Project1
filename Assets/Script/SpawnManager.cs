using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Pool Settings")]
    [SerializeField] private List<GameObject> itemPrefabs;
    [SerializeField] private int poolSizePerItem = 5;

    [Header("Spawn Settings")]
    [SerializeField] private float spawnRate = 3f;

    private List<GameObject> pooledItems = new();
    private List<Transform> autoSpawnPoints = new();
    private float spawnTimer;

    private GameObject poolContainer;

    public bool i = false;

    void OnEnable()
    {
        ResetSpawn();
    }

    private void ResetSpawn()
    {
        if (poolContainer != null)
        {
            Destroy(poolContainer);
        }

        pooledItems.Clear();
        autoSpawnPoints.Clear();

        GetSpawnPointsFromChildren();
        GenerateDynamicPool();
    }

    void Update()
    {
        if(i == true) { ResetSpawn(); i = false; }
        if (pooledItems.Count > 0 && autoSpawnPoints.Count > 0)
        {
            HandleSpawning();
        }
    }

    private void GetSpawnPointsFromChildren()
    {
        autoSpawnPoints.Clear();
        foreach (Transform child in transform)
        {
            autoSpawnPoints.Add(child);
        }
    }

    private void GenerateDynamicPool()
    {
        if (itemPrefabs == null || itemPrefabs.Count == 0) return;

        pooledItems.Clear();
        poolContainer = new GameObject("Runtime_Pool_Container");

        foreach (GameObject prefab in itemPrefabs)
        {
            for (int i = 0; i < poolSizePerItem; i++)
            {
                GameObject obj = Instantiate(prefab, poolContainer.transform);
                obj.SetActive(false);
                pooledItems.Add(obj);
            }
        }
    }

    private void HandleSpawning()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnRate)
        {
            spawnTimer = 0f;
            SpawnRandomItem();
        }
    }

    private void SpawnRandomItem()
    {
        // 1. Lọc các vị trí THỰC SỰ TRỐNG (Không có item nào đang gán vào điểm đó)
        List<Transform> emptyPoints = GetEmptySpawnPoints();

        if (emptyPoints.Count == 0)
        {
            //Debug.LogWarning("[SpawnManager] Tất cả các điểm spawn đều đã có item! Không spawn thêm để tránh đè nhau.");
            return;
        }

        // 2. Lọc các item đang rảnh trong Pool
        List<GameObject> availableItems = new List<GameObject>();
        foreach (GameObject item in pooledItems)
        {
            if (!item.activeInHierarchy)
            {
                availableItems.Add(item);
            }
        }

        // 3. Tiến hành spawn nếu thỏa mãn cả 2 điều kiện
        if (availableItems.Count > 0)
        {
            int randomItemIndex = Random.Range(0, availableItems.Count);
            GameObject itemToSpawn = availableItems[randomItemIndex];

            // Chọn ngẫu nhiên 1 điểm trong số các điểm đang TRỐNG
            int randomPointIndex = Random.Range(0, emptyPoints.Count);
            Transform targetPoint = emptyPoints[randomPointIndex];

            // Gán liên kết để đánh dấu điểm này đã bận
            Gems gemScript = itemToSpawn.GetComponent<Gems>();
            if (gemScript != null)
            {
                gemScript.currentSpawnPoint = targetPoint;
            }

            // Đặt vị trí và kích hoạt
            itemToSpawn.transform.position = targetPoint.position;
            itemToSpawn.SetActive(true);
        }
    }

    // Hàm bổ trợ để quét xem điểm nào đang trống
    private List<Transform> GetEmptySpawnPoints()
    {
        List<Transform> emptyList = new List<Transform>(autoSpawnPoints);

        // Duyệt qua tất cả item trong pool, nếu item nào đang HIỆN, 
        // chứng tỏ cái điểm 'currentSpawnPoint' của nó đang BẬN -> Xóa khỏi danh sách trống
        foreach (GameObject item in pooledItems)
        {
            if (item.activeInHierarchy)
            {
                Gems gemScript = item.GetComponent<Gems>();
                if (gemScript != null && gemScript.currentSpawnPoint != null)
                {
                    emptyList.Remove(gemScript.currentSpawnPoint);
                }
            }
        }

        return emptyList;
    }

    private void OnDisable()
    {

        // Khi rời Level, hủy tận gốc Container chứa bản sao cũ
        if (poolContainer != null)
        {
            Destroy(poolContainer);
        }

        // Giải phóng danh sách tham chiếu để tránh hiện tượng rác bộ nhớ (Null Reference) ở Scene sau
        pooledItems.Clear();
        autoSpawnPoints.Clear();
    }
}