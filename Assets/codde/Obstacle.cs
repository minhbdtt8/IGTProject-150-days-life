using UnityEngine;
using UnityEngine.SceneManagement; // Import thư viện để load lại scene

public class Obstacle : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Kiểm tra nếu player chạm vào
        {
            Debug.Log("🚨 Player hit an obstacle! Restarting...");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Restart level
        }
    }
}
