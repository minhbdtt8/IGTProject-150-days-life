using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;                  // Giá trị nhập từ phím trái/phải
    private float speed = 8f;                  // Tốc độ di chuyển
    private bool isFacingRight = true;         // Đang nhìn về bên phải?

    [SerializeField] private Rigidbody2D rb;               // Rigidbody2D gắn vào Player
    [SerializeField] private Animator playerAnimator;      // Animator của Player

    void Update()
    {
        // Lấy input trái/phải (-1, 0, 1)
        horizontal = Input.GetAxisRaw("Horizontal");

        // Chỉ set isRunning = true nếu có di chuyển rõ ràng (tránh lỗi animation)
        playerAnimator.SetBool("isRunning", Mathf.Abs(horizontal) > 0.01f);

        // Xoay hướng nhân vật nếu cần
        Flip();
    }

    void FixedUpdate()
    {
        // Di chuyển nhân vật theo hướng input
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private void Flip()
    {
        // Nếu hướng hiện tại khác với hướng đang bấm → lật hướng
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
