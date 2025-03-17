


//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class Coincount : MonoBehaviour
//{
//    public int count; // Số tiền hiện có của player
//    public Text text; // Hiển thị số tiền
//    private bool canInteract = false; // Xác định xem player có thể tương tác với Work hay không

//    void Start()
//    {
//        count = PlayerPrefs.GetInt("amount");
//        UpdateText();
//    }

//    void Update()
//    {
//        // Nếu player đang ở gần Work và nhấn phím E
//        if (canInteract && Input.GetKeyDown(KeyCode.E))
//        {
//            count += 1;
//            PlayerPrefs.SetInt("amount", count);
//            UpdateText();
//            Debug.Log("Player đã tương tác với đối tượng Work và nhận được 1 coin.");
//        }
//    }

//    void OnTriggerEnter2D(Collider2D other)
//    {
//        // Kiểm tra va chạm với đối tượng có tag "Work"
//        if (other.CompareTag("Work"))
//        {
//            canInteract = true;
//            Debug.Log("Player đã vào phạm vi tương tác của Work.");
//        }
//        // Kiểm tra va chạm với đối tượng có tag "pay"
//        else if (other.CompareTag("pay"))
//        {
//            PayObject payObject = other.GetComponent<PayObject>();
//            if (payObject != null)
//            {
//                count -= payObject.cost;

//                // Đảm bảo tiền không xuống dưới 0
//                if (count < 0)
//                {
//                    count = 0;
//                }

//                PlayerPrefs.SetInt("amount", count);
//                UpdateText();

//                Debug.Log("Player chạm vào vật thể pay, trừ tiền: " + payObject.cost);
//            }
//        }
//    }

//    void OnTriggerExit2D(Collider2D other)
//    {
//        // Kiểm tra khi player rời khỏi phạm vi của Work
//        if (other.CompareTag("Work"))
//        {
//            canInteract = false;
//            Debug.Log("Player đã rời khỏi phạm vi tương tác của Work.");
//        }
//    }

//    void UpdateText()
//    {
//        text.text = count.ToString();
//    }
//}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Để reset level

public class Coincount : MonoBehaviour
{
    public int count; // Số tiền hiện có của player
    public int energy = 100; // Điểm năng lượng
    public int health = 100; // Điểm sức khỏe
    public int happy = 100; // Điểm hạnh phúc

    public Text countText; // Hiển thị số tiền
    public Text energyText; // Hiển thị năng lượng
    public Text healthText; // Hiển thị sức khỏe
    public Text happyText; // Hiển thị hạnh phúc

    private bool canInteract = false; // Xác định xem player có thể tương tác với Work hay không
    private bool canSleep = false; // Xác định xem player có thể ngủ tại SleepPoint
    public GameManager gameManager; // Tham chiếu đến GameManager để thực hiện hành động ngủ

    void Start()
    {
        // Lấy giá trị từ PlayerPrefs nếu có
        count = PlayerPrefs.GetInt("amount", 0);

        // Cập nhật giao diện ban đầu
        UpdateUI();
    }

    void Update()
    {
        // Kiểm tra nếu player đang ở gần Work và nhấn phím E
        if (canInteract && Input.GetKeyDown(KeyCode.E))
        {
            WorkAction();
        }

        // Kiểm tra nếu bất kỳ giá trị nào giảm xuống 0
        if (energy <= 0 || health <= 0 || happy <= 0)
        {
            ResetLevel();
        }
        if (canSleep && Input.GetKeyDown(KeyCode.E))
        {
            gameManager.Sleep();
        }
    }

  
        void OnTriggerEnter2D(Collider2D other)
    {
        // Kiểm tra va chạm với đối tượng có tag "Work"
        if (other.CompareTag("Work"))
        {
            canInteract = true;
            Debug.Log("Player đã vào phạm vi tương tác của Work.");
        }
        // Kiểm tra va chạm với đối tượng có tag "pay"
        else if (other.CompareTag("pay"))
        {
            PayObject payObject = other.GetComponent<PayObject>();
            if (payObject != null)
            {
                count -= payObject.cost;

                // Đảm bảo tiền không xuống dưới 0
                if (count < 0)
                {
                    count = 0;
                }

                PlayerPrefs.SetInt("amount", count);
                UpdateUI();

                Debug.Log("Player chạm vào vật thể pay, trừ tiền: " + payObject.cost);
            }
        }
        if (other.CompareTag("SleepPoint"))
        {
            canSleep = true;
            Debug.Log("Player đã vào phạm vi SleepPoint.");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Work"))
        {
            canInteract = false;
            Debug.Log("Player đã rời khỏi phạm vi tương tác của Work.");
        }
        else if (other.CompareTag("SleepPoint"))
        {
            canSleep = false;
            Debug.Log("Player đã rời khỏi phạm vi SleepPoint.");
        }
    }

    void WorkAction()
    {
        count += 100; // Kiếm tiền
        energy -= 10; // Mất năng lượng
        happy -= 5; // Mất hạnh phúc

        // Đảm bảo các giá trị không âm
        energy = Mathf.Max(energy, 0);
        happy = Mathf.Max(happy, 0);

        // Lưu số tiền và cập nhật giao diện
        PlayerPrefs.SetInt("amount", count);
        UpdateUI();

        Debug.Log("Player kiếm tiền, năng lượng và hạnh phúc giảm.");
    }

    public void UpdateUI()
    {
        // Cập nhật giao diện hiển thị số tiền, năng lượng, sức khỏe, và hạnh phúc
        // Đảm bảo các Text UI được cập nhật theo logic của GameManager
        if (countText != null) countText.text = "Money: " + count;
        if (energyText != null) energyText.text = "Energy: " + energy;
        if (healthText != null) healthText.text = "Health: " + health;
        if (happyText != null) happyText.text = "Happy: " + happy;
    }


    void ResetLevel()
    {
        Debug.Log("Player hết điểm Energy, Health, hoặc Happy. Reset level!");

        // Đặt lại điểm về giá trị ban đầu
        count = 0;
        energy = 100;
        health = 100;
        happy = 100;

        PlayerPrefs.SetInt("amount", count);
        UpdateUI();

        // Reset scene (nếu cần thiết)
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
