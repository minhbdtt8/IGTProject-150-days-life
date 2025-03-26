using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class GameManager1 : MonoBehaviour
{
    public static GameManager1 Instance { get; private set; }

    public int playerBaseHealth = 20;
    public int enemyBaseHealth = 20;
    public int playerMoney = 50;
    public int moneyIncreaseRate = 1;

    public TextMeshProUGUI playerBaseHealthText;
    public TextMeshProUGUI enemyBaseHealthText;
    public TextMeshProUGUI moneyText;

    public GameObject trianglePrefab;
    public GameObject squarePrefab;
    public GameObject hexagonPrefab;

    public Transform playerSpawnPoint;
    public Transform enemySpawnPoint;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateUI();
        InvokeRepeating(nameof(IncreaseMoneyOverTime), 1f, 1f);
        StartCoroutine(AI_SpawnUnits());
    }

    private void UpdateUI()
    {
        if (playerBaseHealthText) playerBaseHealthText.text = "Base HP: " + playerBaseHealth;
        if (enemyBaseHealthText) enemyBaseHealthText.text = "Enemy HP: " + enemyBaseHealth;
        if (moneyText) moneyText.text = "Money: $" + playerMoney;
    }

    public void TakeDamage(bool isPlayerBase)
    {
        if (isPlayerBase)
        {
            playerBaseHealth--;
            if (playerBaseHealth <= 0)
            {
                Debug.Log("Bạn thua!");
                RestartGame();
            }
        }
        else
        {
            enemyBaseHealth--;
            if (enemyBaseHealth <= 0)
            {
                Debug.Log("Bạn thắng!");
                WinGame();
            }
        }
        UpdateUI();
    }


    private void IncreaseMoneyOverTime()
    {
        playerMoney += moneyIncreaseRate;
        UpdateUI();
    }

    public void SpawnUnit(int unitType, bool isPlayer)
    {
        if (isPlayer && playerMoney < 10) return; // Kiểm tra tiền

        GameObject unitPrefab = null;

        if (unitType == 1) unitPrefab = trianglePrefab;
        else if (unitType == 2) unitPrefab = squarePrefab;
        else if (unitType == 3) unitPrefab = hexagonPrefab;

        if (unitPrefab == null) return;

        Vector3 spawnPosition = isPlayer ? playerSpawnPoint.position : enemySpawnPoint.position;
        GameObject unit = Instantiate(unitPrefab, spawnPosition, Quaternion.identity);

        UnitController unitController = unit.GetComponent<UnitController>();
        if (unitController != null)
        {
            unitController.isPlayerUnit = isPlayer;
        }

        if (isPlayer)
        {
            playerMoney -= 10;
            UpdateUI();
        }
    }

    private IEnumerator AI_SpawnUnits()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(2f, 5f));

            int unitType = Random.Range(1, 4); // AI chọn quân ngẫu nhiên
            SpawnUnit(unitType, false);
        }
    }

    private void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    private void WinGame()
    {
        Debug.Log("Chuyển sang màn hình chiến thắng...");
    }
}
