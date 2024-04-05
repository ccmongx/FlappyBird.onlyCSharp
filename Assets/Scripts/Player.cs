using UnityEngine;

public class Player : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] sprites;
    private int spriteIndex;

    [SerializeField] private float strength = 5f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float tilt = 5f;

    [SerializeField] private string Obstacle = "Obstacle";
    [SerializeField] private string Scoring = "Scoring";

    private Vector3 direction;
    private bool isMovingDown = false; // Biến kiểm tra xem nhân vật có đang di chuyển xuống không

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(AnimateSprite), 0.15f, 0.15f);
    }

    private void OnEnable()
    {
        Vector3 position = transform.position;
        position.y = 0f;
        transform.position = position;
        direction = Vector3.up * strength; // Bắt đầu di chuyển lên trên
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            direction = Vector3.down * strength; // Di chuyển xuống khi thao tác
            isMovingDown = true; // Đánh dấu nhân vật đang di chuyển xuống
            CancelInvoke("ResetDirection"); // Hủy bỏ lệnh đặt lại hướng nếu đang diễn ra
            Invoke("ResetDirection", 0.17f); // Đặt lại hướng
        }

        if (!isMovingDown)
        {
            direction = Vector3.up * strength; // Nếu không có thao tác, tiếp tục di chuyển lên trên
        }

        direction.y += gravity * Time.deltaTime; // Áp dụng trọng lực
        transform.position += direction * Time.deltaTime; // Cập nhật vị trí

        // Nghiêng nhân vật dựa trên hướng di chuyển
        Vector3 rotation = transform.eulerAngles;
        rotation.z = direction.y * tilt;
        transform.eulerAngles = rotation;
    }

    private void ResetDirection()
    {
        isMovingDown = false; // Đặt lại trạng thái di chuyển xuống
    }

    private void AnimateSprite()
    {
        spriteIndex++;

        if (spriteIndex >= sprites.Length)
        {
            spriteIndex = 0;
        }

        if (spriteIndex < sprites.Length && spriteIndex >= 0)
        {
            spriteRenderer.sprite = sprites[spriteIndex];
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(Obstacle))
        {
            FindObjectOfType<GameManager>().GameOver();
        }
        else if (other.gameObject.CompareTag(Scoring))
        {
            FindObjectOfType<GameManager>().IncreaseScore();
        }
    }
}
