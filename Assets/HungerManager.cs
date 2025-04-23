using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HungerManager : MonoBehaviour
{
    public int hunger = 100; // Chỉ số đói
    public Text hungerText;
    public Text favoriteFoodText;

    private string[] foodOptions = { "Pizza", "Burger", "Sushi", "Salad", "Pasta" };
    private string favoriteFood;
    private float timeCounter = 0f;
    private float hungerDecreaseInterval = 100f; // Giảm đói mỗi 100 giây

    void Start()
    {
        ChooseFavoriteFood();
        UpdateUI();
    }

    void Update()
    {
        timeCounter += Time.deltaTime;
        if (timeCounter >= hungerDecreaseInterval)
        {
            DecreaseHunger();
            timeCounter = 0f;
        }
    }

    void ChooseFavoriteFood()
    {
        favoriteFood = foodOptions[Random.Range(0, foodOptions.Length)];
        favoriteFoodText.text = "Today's Favorite Food: " + favoriteFood;
    }

    public void EatFood(string food)
    {
        int hungerIncrease = (food == favoriteFood) ? 30 : 15;
        hunger = Mathf.Min(hunger + hungerIncrease, 100);
        UpdateUI();
    }

    void DecreaseHunger()
    {
        hunger = Mathf.Max(hunger - 10, 0);
        if (hunger == 0)
        {
            Debug.Log("Player is starving! Quitting game...");
            QuitGame();
        }
        UpdateUI();
    }

    public void UpdateUI()
    {
        hungerText.text = "Hunger: " + hunger;
    }

    void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
