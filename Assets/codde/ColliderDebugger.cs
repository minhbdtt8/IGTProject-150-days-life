using UnityEngine;

public class ColliderDebugger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(gameObject.name + " (Trigger) va chạm với: " + collision.gameObject.name);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(gameObject.name + " (Collider) va chạm với: " + collision.gameObject.name);
    }
}
