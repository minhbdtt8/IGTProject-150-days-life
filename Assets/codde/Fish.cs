//using UnityEngine;

//public class Fish : MonoBehaviour
//{
//    public int fishValue = 10; // Giá trị của cá
//    private bool isCaught = false;

//    private void OnTriggerEnter2D(Collider2D other)
//    {
//        if (other.CompareTag("Hook") && !isCaught)
//        {
//            isCaught = true;
//            CatchFish();
//        }
//    }

//    void CatchFish()
//    {
//        Debug.Log("Caught a fish worth " + fishValue + " points!");

//        // Tìm FishSpawner trong scene và gọi hàm FishCaught
//        FishSpawner spawner = FindObjectOfType<FishSpawner>();
//        if (spawner != null)
//        {
//            spawner.FishCaught(gameObject);
//        }

//        Destroy(gameObject); // Hoặc disable gameObject
//    }
//}
using UnityEngine;

public class Fish : MonoBehaviour
{
    public int fishValue = 10; // Giá trị của cá
    private bool isCaught = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hook") && !isCaught)
        {
            isCaught = true;
            CatchFish();
        }
    }

    void CatchFish()
    {
        Debug.Log("Caught a fish worth " + fishValue + " points!");

        // Tìm script Coincount để cập nhật happy và energy
        Coincount playerStats = FindObjectOfType<Coincount>();
        if (playerStats != null)
        {
            playerStats.happy += 10; // Cộng 10 điểm happy
            playerStats.energy -= 10; // Trừ 10 điểm năng lượng

            // Đảm bảo các giá trị không vượt quá giới hạn hoặc xuống dưới 0
            playerStats.happy = Mathf.Clamp(playerStats.happy, 0, 100);
            playerStats.energy = Mathf.Clamp(playerStats.energy, 0, 100);

            // Cập nhật giao diện (nếu có)
            playerStats.UpdateUI();
        }

        // Tìm FishSpawner trong scene và gọi hàm FishCaught
        FishSpawner spawner = FindObjectOfType<FishSpawner>();
        if (spawner != null)
        {
            spawner.FishCaught(gameObject);
        }

        Destroy(gameObject); // Hoặc disable gameObject
    }
}
