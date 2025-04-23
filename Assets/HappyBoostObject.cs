using UnityEngine;

public class HappyBoostObject : MonoBehaviour
{
    public Coincount playerStats;
    public GameManager gameManager;

    private bool canInteract = false;

    void Update()
    {
        if (canInteract && Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = false;
        }
    }

    void Interact()
    {
        if (playerStats.count >= 500)
        {
            playerStats.count -= 500;
            playerStats.happy = Mathf.Min(playerStats.happy + 20, 100);
            playerStats.UpdateUI();
            gameManager.Sleep();
        }
        else
        {
            Debug.Log("Không đủ tiền để tăng Happy.");
        }
    }
}
