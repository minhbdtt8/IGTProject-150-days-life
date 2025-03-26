using UnityEngine;

public class PlayerAssassination : MonoBehaviour
{
    private GameObject enemyInRange;

    void Update()
    {
        if (enemyInRange != null && Input.GetKeyDown(KeyCode.E))
        {
            Destroy(enemyInRange); // Chỉ hủy Enemy khi nhấn E
            enemyInRange = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            enemyInRange = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            enemyInRange = null;
        }
    }
}

