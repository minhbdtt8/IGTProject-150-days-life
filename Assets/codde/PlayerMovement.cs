using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;
    private bool isFacingRight = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator playerAnimator;

    private inventory playerInventory; // Tham chiếu đến inventory

    void Start()
    {
        // Tìm inventory gắn trên cùng object hoặc cha
        playerInventory = GetComponent<inventory>();
        if (playerInventory == null)
        {
            playerInventory = GetComponentInChildren<inventory>();
        }
    }

    void Update()
    {
        // Nếu đang cầm cần → không cho di chuyển
        if (playerInventory != null && playerInventory.isHoldingRod)
        {
            horizontal = 0f;
        }
        else
        {
            horizontal = Input.GetAxisRaw("Horizontal");
        }

        playerAnimator.SetBool("isRunning", Mathf.Abs(horizontal) > 0.01f);
        Flip();
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}

