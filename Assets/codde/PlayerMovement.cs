﻿using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;
    private bool isFacingRight = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator playerAnimator;

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        

        // Cập nhật animation di chuyển trái/phải
        playerAnimator.SetBool("MoveLeft", horizontal < 0);
        playerAnimator.SetBool("MoveRight", horizontal > 0);

        Flip();
    }

    private void FixedUpdate()
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
