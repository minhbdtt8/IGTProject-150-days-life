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

    private int lastPaidDay = 0; // Lưu ngày cuối cùng trả tiền
    public GameObject dialogueBox; // Hộp thoại để xác nhận thanh toán
    public Text dialogueText; // Nội dung hộp thoại
    private PayObject pendingPayObject; // Lưu trữ object cần thanh toán

    private bool isDialogueActive = false; // Kiểm tra xem Panel có đang mở không
    public PlayerMovement playerMovement; // Tham chiếu đến script điều khiển người chơi


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
        if (dialogueBox.activeSelf && Input.GetKeyDown(KeyCode.E))
        {
            ConfirmPayment();
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
            if (payObject != null && gameManager.currentDay >= lastPaidDay + 30)
            {
                Debug.Log("Đã đến hạn thanh toán, mở hộp thoại!");

                // Hiển thị hộp thoại
                dialogueBox.SetActive(true);
                dialogueText.text = "Bạn cần thanh toán " + payObject.cost + " tiền. Nhấn E để xác nhận.";

                pendingPayObject = payObject; // Lưu object để xử lý khi nhấn E
                isDialogueActive = true; // Đánh dấu hộp thoại đang mở

                // **Dừng di chuyển ngay lập tức**
                StopPlayerMovement();
            }
        }



        if (other.CompareTag("SleepPoint"))
        {
            canSleep = true;
            Debug.Log("Player đã vào phạm vi SleepPoint.");
        }
    }
    void StopPlayerMovement()
    {
        // **Tắt điều khiển**
        playerMovement.enabled = false;

        // **Nếu Player dùng Rigidbody2D, đặt vận tốc về 0**
        Rigidbody2D rb = playerMovement.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX; // **Đóng băng di chuyển**
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
    void ConfirmPayment()
    {
        if (pendingPayObject != null)
        {
            count -= pendingPayObject.cost;
            count = Mathf.Max(count, 0); // Đảm bảo không xuống âm

            PlayerPrefs.SetInt("amount", count);
            lastPaidDay = gameManager.currentDay; // Cập nhật ngày cuối thanh toán
            UpdateUI();

            Debug.Log("Đã thanh toán " + pendingPayObject.cost + " vào ngày " + lastPaidDay);
        }

        // Đóng hộp thoại
        dialogueBox.SetActive(false);
        pendingPayObject = null;
        isDialogueActive = false;

        // **Bật lại điều khiển**
        playerMovement.enabled = true;

        // **Mở khóa di chuyển**
        Rigidbody2D rb = playerMovement.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation; // Chỉ khóa xoay, không khóa di chuyển
        }
    }



}
