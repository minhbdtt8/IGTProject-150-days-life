using System.Collections;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    public float speed = 2f;               // Tốc độ di chuyển của cá
    public Transform spawnArea;            // Khu vực giới hạn cá di chuyển
    private Vector2 minBounds;             // Giới hạn dưới của khu vực spawn
    private Vector2 maxBounds;             // Giới hạn trên của khu vực spawn
    private Vector2 targetPosition;        // Vị trí mục tiêu của cá

    private void Start()
    {
        if (spawnArea != null)
        {
            // Xác định phạm vi của khu vực spawn
            minBounds = new Vector2(
                spawnArea.position.x - spawnArea.localScale.x / 2,
                spawnArea.position.y - spawnArea.localScale.y / 2
            );
            maxBounds = new Vector2(
                spawnArea.position.x + spawnArea.localScale.x / 2,
                spawnArea.position.y + spawnArea.localScale.y / 2
            );
        }

        SetRandomTargetPosition();
    }

    private void Update()
    {
        if (spawnArea == null) return; // Đảm bảo khu vực spawn tồn tại

        // Di chuyển cá về hướng mục tiêu
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Nếu cá đã đến gần mục tiêu, chọn một vị trí mới
        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            SetRandomTargetPosition();
        }
    }

    // Chọn vị trí ngẫu nhiên trong khu vực spawn
    void SetRandomTargetPosition()
    {
        targetPosition = new Vector2(
            Random.Range(minBounds.x, maxBounds.x),
            Random.Range(minBounds.y, maxBounds.y)
        );
    }

    // Đảm bảo cá không ra ngoài khu vực spawn
    private void LateUpdate()
    {
        transform.position = new Vector2(
            Mathf.Clamp(transform.position.x, minBounds.x, maxBounds.x),
            Mathf.Clamp(transform.position.y, minBounds.y, maxBounds.y)
        );
    }
}
