using UnityEngine;

public class GhostEffect : MonoBehaviour
{
    [SerializeField] private float fadeSpeed = 3f; // Tốc độ mờ dần
    private SpriteRenderer sr;
    private Color alphaColor;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Hàm này được gọi mỗi khi bóng ma được lấy ra từ Pool
    public void ActivateGhost(Sprite currentSprite, Vector3 position, Quaternion rotation, Vector3 scale, Color ghostColor)
    {
        transform.position = position;
        transform.rotation = rotation;
        transform.localScale = scale;

        sr.sprite = currentSprite;

        // Đặt màu ban đầu và Reset độ mờ (Alpha) về mức bạn muốn (ví dụ: 0.6)
        alphaColor = ghostColor;
        alphaColor.a = 0.6f;
        sr.color = alphaColor;

        gameObject.SetActive(true);
    }

    private void Update()
    {
        // Làm mờ dần theo thời gian
        alphaColor.a -= fadeSpeed * Time.deltaTime;
        sr.color = alphaColor;

        // Khi đã mờ hoàn toàn, ẩn đi để trả về Pool
        if (alphaColor.a <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}