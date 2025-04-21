using UnityEngine;

public class FoodItem : MonoBehaviour
{
    public string foodName;
    private bool isPlayerNearby = false;
    private GameObject player;

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Test nút E");
            HungerManager hungerManager = player.GetComponent<HungerManager>();
            if (hungerManager != null)
            {
                hungerManager.EatFood(foodName);
                Destroy(gameObject);
                Debug.Log("Đang đứng gần và đã nhấn E");

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            player = other.gameObject;
            Debug.Log("Player đứng gần món ăn: " + foodName);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            player = null;
        }
    }
}
