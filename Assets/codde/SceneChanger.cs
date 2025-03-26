using UnityEngine;
using UnityEngine.SceneManagement; // Import thư viện quản lý scene

public class SceneChanger : MonoBehaviour
{
    public string sceneName; // Nhập tên scene trong Inspector

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Kiểm tra nếu Player chạm vào
        {
            Debug.Log("🚀 Đang chuyển sang scene: " + sceneName);
            SceneManager.LoadScene(sceneName); // Load scene được chọn
        }
    }
}

