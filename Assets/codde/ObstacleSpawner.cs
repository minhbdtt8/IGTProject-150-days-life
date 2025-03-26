using UnityEngine;
using System.Collections;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab; // Prefab của vật cản
    public Transform player; // Gán Player trong Inspector
    public float spawnDistance = 10f; // Spawn phía trước player
    public float minSpawnTime = 1.5f; // Khoảng thời gian spawn nhỏ nhất
    public float maxSpawnTime = 4f; // Khoảng thời gian spawn lớn nhất
    public LayerMask groundLayer; // Layer để nhận diện mặt đất

    void Start()
    {
        StartCoroutine(SpawnObstacleRandomly());
    }

    IEnumerator SpawnObstacleRandomly()
    {
        while (true) // Lặp mãi mãi
        {
            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime)); // Đợi thời gian ngẫu nhiên

            SpawnObstacle();
        }
    }

    void SpawnObstacle()
    {
        // Tìm vị trí mặt đất tại điểm spawn
        Vector3 spawnPosition = new Vector3(player.position.x + spawnDistance, player.position.y + 5f, 0);
        RaycastHit2D hit = Physics2D.Raycast(spawnPosition, Vector2.down, 10f, groundLayer);

        if (hit.collider != null) // Nếu tìm thấy mặt đất
        {
            spawnPosition.y = hit.point.y + 0.5f; // Spawn ngay trên mặt đất

            GameObject newObstacle = Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);

            // Đảm bảo obstacle không bị rơi
            Rigidbody2D rb = newObstacle.GetComponent<Rigidbody2D>();
            if (rb)
            {
                rb.gravityScale = 0; // Không rơi nếu spawn trên platform
            }
        }
        else
        {
            Debug.LogWarning("⚠ Không tìm thấy mặt đất để spawn obstacle!");
        }
    }
}
