using UnityEngine;

public class UnitController : MonoBehaviour
{
    public float speed = 2f;
    public bool isPlayerUnit; // Đảm bảo là public

    private void Update()
    {
        transform.Translate(Vector3.right * (isPlayerUnit ? 1 : -1) * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        UnitController enemyUnit = other.GetComponent<UnitController>();

        if (enemyUnit != null)
        {
            if (isPlayerUnit != enemyUnit.isPlayerUnit) // Gặp quân địch
            {
                if (this.gameObject.tag == enemyUnit.gameObject.tag) // Nếu giống nhau, hủy cả hai
                {
                    Destroy(enemyUnit.gameObject);
                    Destroy(this.gameObject);
                }
                else if (CanDefeat(this.gameObject.tag, enemyUnit.gameObject.tag)) // Nếu thắng
                {
                    Destroy(enemyUnit.gameObject);
                }
                else if (CanDefeat(enemyUnit.gameObject.tag, this.gameObject.tag)) // Nếu thua
                {
                    Destroy(this.gameObject);
                }
            }
        }
        else if ((isPlayerUnit && other.CompareTag("EnemyBase")) || (!isPlayerUnit && other.CompareTag("PlayerBase")))
        {
            GameManager1.Instance.TakeDamage(!isPlayerUnit);
            Destroy(this.gameObject); // Quân biến mất sau khi gây sát thương
        }
    }



    private bool CanDefeat(string attacker, string defender)
    {
        return (attacker == "Triangle" && defender == "Square") ||
               (attacker == "Square" && defender == "Hexagon") ||
               (attacker == "Hexagon" && defender == "Triangle");
    }

}
