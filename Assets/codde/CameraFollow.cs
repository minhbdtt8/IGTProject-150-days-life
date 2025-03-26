using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 0.1f;
    public Vector3 offset = new Vector3(2f, 0f, -10f); // Moves slightly in front of player

    void LateUpdate()
    {
        if (player != null)
        {
            Vector3 targetPosition = new Vector3(player.position.x + offset.x, offset.y, offset.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
        }
    }
}
