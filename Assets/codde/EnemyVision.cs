using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyVision : MonoBehaviour
{
    public Transform visionObject; // Kéo Vision (GameObject) vào đây
    private bool isLookingRight = true;

    void Start()
    {

        if (visionObject == null)
        {
            Debug.LogError("Chưa gán VisionObject!");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Vision va chạm với: " + collision.gameObject.name); // Kiểm tra va chạm

        // Chỉ reset level nếu va chạm với Player
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player bị phát hiện! Reset level...");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reset level
        }
    }
    public void Flip()
    {
        isLookingRight = !isLookingRight;

        // Đổi hướng vùng nhìn theo Enemy
        Vector3 scale = visionObject.localScale;
        scale.x *= -1; // Lật vùng nhìn
        visionObject.localScale = scale;
    }
}
