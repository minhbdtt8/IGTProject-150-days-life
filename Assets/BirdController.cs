using UnityEngine;

public class BirdController : MonoBehaviour
{
    public float jumpForce = 5f;  // Lực nhảy của chim
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Time.timeScale = 0; // Dừng game khi chim va chạm
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            rb.velocity = Vector2.up * jumpForce;
        }

        if (Input.GetKeyDown(KeyCode.R)) // Nhấn R để restart
        {
            Time.timeScale = 1;
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }
    }

}
