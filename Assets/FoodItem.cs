using UnityEngine;

public class FoodItem : MonoBehaviour
{
    public string foodName; // Tên món ăn

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Chạm vào: " + other.gameObject.name); // Kiểm tra xem có va chạm không

        if (other.CompareTag("Player")) // Kiểm tra player có chạm không
        {
            Debug.Log("Player chạm vào đồ ăn: " + foodName);

            HungerManager hungerManager = other.GetComponent<HungerManager>();
            if (hungerManager != null)
            {
                hungerManager.EatFood(foodName);
                Destroy(gameObject); // Xóa object sau khi ăn
            }
        }
    }
}

