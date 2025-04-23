using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Coincount : MonoBehaviour
{
    public int count;
    public int energy = 100;
    public int health = 100;
    public int happy = 100;

    private int lastPaidDay = 0;
    public GameObject dialogueBox;
    public Text dialogueText;
    private PayObject pendingPayObject;

    private bool isDialogueActive = false;
    public PlayerMovement playerMovement;

    public Text countText;
    public Text energyText;
    public Text healthText;
    public Text happyText;

    private bool canInteract = false;
    private bool canSleep = false;
    public GameManager gameManager;

    // Làm việc có timer
    public GameObject workPanel;
    public Text workTimerText;
    private Coroutine workCoroutine = null;
    private bool isWorking = false;

    void Start()
    {
        count = PlayerPrefs.GetInt("amount", 0);
        UpdateUI();
    }

    void Update()
    {
        Debug.Log("canInteract: " + canInteract);

        if (!isWorking && canInteract && Input.GetKeyDown(KeyCode.E))
        {
            StartWork();
        }
        else if (isWorking && Input.GetKeyDown(KeyCode.E))
        {
            StopWorkEarly();
        }

        if (energy <= 0 || health <= 0 || happy <= 0)
        {
            ResetLevel();
        }

        if (canSleep && Input.GetKeyDown(KeyCode.E))
        {
            //gameManager.Sleep(SleepType.Normal);
        }

        if (dialogueBox.activeSelf && Input.GetKeyDown(KeyCode.E))
        {
            ConfirmPayment();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Work"))
        {
            canInteract = true;
            Debug.Log("Player đã vào phạm vi tương tác của Work.");
        }
        else if (other.CompareTag("pay"))
        {
            PayObject payObject = other.GetComponent<PayObject>();
            if (payObject != null && gameManager.currentDay >= lastPaidDay + 30)
            {
                dialogueBox.SetActive(true);
                dialogueText.text = "Bạn cần thanh toán " + payObject.cost + " tiền. Nhấn E để xác nhận.";
                pendingPayObject = payObject;
                isDialogueActive = true;
                StopPlayerMovement();
            }
        }
        else if (other.CompareTag("SleepPoint"))
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

    void StopPlayerMovement()
    {
        playerMovement.enabled = false;
        Rigidbody2D rb = playerMovement.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        }
    }

    void ResumePlayerMovement()
    {
        playerMovement.enabled = true;
        Rigidbody2D rb = playerMovement.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    void StartWork()
    {
        Debug.Log("Bắt đầu làm việc!");

        workPanel.SetActive(true);
        isWorking = true;

        StopPlayerMovement();
        workCoroutine = StartCoroutine(WorkCountdown(60));
    }

    IEnumerator WorkCountdown(int seconds)
    {
        int remaining = seconds;
        while (remaining > 0)
        {
            workTimerText.text = "Đang làm việc: " + remaining + "s";
            yield return new WaitForSeconds(1f);
            remaining--;
        }

        FinishWork();
    }

    void FinishWork()
    {
        Debug.Log("Hoàn tất công việc!");

        count += 100;
        energy -= 10;
        happy -= 5;

        energy = Mathf.Max(0, energy);
        happy = Mathf.Max(0, happy);
        PlayerPrefs.SetInt("amount", count);
        UpdateUI();

        isWorking = false;
        workPanel.SetActive(false);
        ResumePlayerMovement();
    }

    void StopWorkEarly()
    {
        Debug.Log("Ngừng làm việc giữa chừng!");

        if (workCoroutine != null)
            StopCoroutine(workCoroutine);

        count -= 10;
        energy -= 10;

        count = Mathf.Max(0, count);
        energy = Mathf.Max(0, energy);

        PlayerPrefs.SetInt("amount", count);
        UpdateUI();

        isWorking = false;
        workPanel.SetActive(false);
        ResumePlayerMovement();
    }

    void ConfirmPayment()
    {
        if (pendingPayObject != null)
        {
            if (count >= pendingPayObject.cost)
            {
                // Đủ tiền → trừ tiền và thanh toán
                count -= pendingPayObject.cost;
                count = Mathf.Max(count, 0);
                PlayerPrefs.SetInt("amount", count);
                lastPaidDay = gameManager.currentDay;
                UpdateUI();
                Debug.Log("Đã thanh toán " + pendingPayObject.cost + " vào ngày " + lastPaidDay);
            }
            else
            {
                Debug.Log("Không đủ tiền để thanh toán! Thoát game.");
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
            }
        }

        dialogueBox.SetActive(false);
        pendingPayObject = null;
        isDialogueActive = false;
        ResumePlayerMovement();
    }


    void ResetLevel()
    {
        Debug.Log("Player hết điểm Energy, Health, hoặc Happy. Reset level!");
        count = 0;
        energy = 100;
        health = 100;
        happy = 100;

        PlayerPrefs.SetInt("amount", count);
        UpdateUI();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void UpdateUI()
    {
        if (countText != null) countText.text = "Money: " + count;
        if (energyText != null) energyText.text = "Energy: " + energy;
        if (healthText != null) healthText.text = "Health: " + health;
        if (happyText != null) happyText.text = "Happy: " + happy;
    }
}
