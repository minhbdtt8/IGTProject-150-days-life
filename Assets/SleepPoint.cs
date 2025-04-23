using UnityEngine;

public enum SleepType
{
    Normal,
    Luxury
}

public class SleepPoint : MonoBehaviour
{
    public SleepType sleepType; // Chọn loại giấc ngủ trong Inspector
    public GameManager gameManager; // Kéo GameManager vào từ Inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.Sleep();
        }

    }
}
