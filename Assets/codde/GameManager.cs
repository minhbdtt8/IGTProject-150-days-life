//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.SceneManagement;

//public class GameManager : MonoBehaviour
//{
//    public int currentDay = 1; // Ngày hiện tại
//    public float dayDuration = 1800f; // 30 phút (thời gian thực) = 1800 giây
//    private float timer; // Bộ đếm thời gian trong ngày

//    public Text dayText; // Hiển thị số ngày
//    public Coincount playerStats; // Tham chiếu đến script Coincount để reset năng lượng, máu, v.v.

//    void Start()
//    {
//        timer = dayDuration;
//        UpdateDayUI();
//    }

//    void Update()
//    {
//        timer -= Time.deltaTime;

//        if (timer <= 0)
//        {
//            // Nếu hết giờ mà player không ngủ, reset level
//            ResetLevel();
//        }
//    }

//    public void Sleep()
//    {
//        // Chuyển sang ngày mới
//        currentDay++;
//        timer = dayDuration; // Reset thời gian của ngày mới

//        // Hồi phục năng lượng và máu
//        playerStats.energy = 100;
//        if (playerStats.health < 100)
//        {
//            playerStats.health += 10; // Máu hồi phục 10 điểm
//        }
//        playerStats.UpdateUI(); // Cập nhật giao diện hiển thị các chỉ số

//        UpdateDayUI();
//        Debug.Log("Người chơi đã ngủ và chuyển sang ngày mới.");
//    }

//    void ResetLevel()
//    {
//        Debug.Log("Hết ngày mà không ngủ. Reset level.");
//        currentDay = 0;
//        timer = dayDuration;

//        // Đặt lại tiền và ngày của player
//        playerStats.count = 0;
//        playerStats.energy = 100;
//        playerStats.health = 100;
//        playerStats.happy = 100;
//        playerStats.UpdateUI();

//        // Tải lại scene
//        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
//    }

//    void UpdateDayUI()
//    {
//        if (dayText != null)
//        {
//            dayText.text = "Day: " + currentDay;
//        }
//    }
//}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int currentDay = 1; // Ngày hiện tại
    public float dayDuration = 1800f; // 30 phút (thời gian thực) = 1800 giây
    private float timer; // Bộ đếm thời gian trong ngày

    public Text dayText; // Hiển thị số ngày
    public Text clockText; // Hiển thị đồng hồ
    public Coincount playerStats; // Tham chiếu đến script Coincount để reset năng lượng, máu, v.v.

    void Start()
    {
        timer = dayDuration;
        UpdateDayUI();
    }

    void Update()
    {
        timer -= Time.deltaTime;

        // Cập nhật giao diện đồng hồ
        UpdateClockUI();

        if (timer <= 0)
        {
            // Nếu hết giờ mà player không ngủ, reset level
            ResetLevel();
        }
    }

    public void Sleep()
    {
        // Chuyển sang ngày mới
        currentDay++;
        timer = dayDuration; // Reset thời gian của ngày mới

        // Hồi phục năng lượng và máu
        playerStats.energy = 100;
        if (playerStats.health < 100)
        {
            playerStats.health += 10; // Máu hồi phục 10 điểm
        }
        playerStats.UpdateUI(); // Cập nhật giao diện hiển thị các chỉ số

        UpdateDayUI();
        Debug.Log("Người chơi đã ngủ và chuyển sang ngày mới.");
    }

    void ResetLevel()
    {
        Debug.Log("Hết ngày mà không ngủ. Reset level.");
        currentDay = 0;
        timer = dayDuration;

        // Đặt lại tiền và ngày của player
        playerStats.count = 0;
        playerStats.energy = 100;
        playerStats.health = 100;
        playerStats.happy = 100;
        playerStats.UpdateUI();

        // Tải lại scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    void UpdateDayUI()
    {
        if (dayText != null)
        {
            dayText.text = "Day: " + currentDay;
        }
    }

    void UpdateClockUI()
    {
        if (clockText != null)
        {
            // Tính thời gian đã trôi qua trong ngày
            float timeLeft = dayDuration - timer;
            float normalizedTime = timeLeft / dayDuration; // Tỉ lệ thời gian đã trôi qua
            float totalMinutes = normalizedTime * 1440f; // Một ngày có 1440 phút (24 giờ)

            // Thêm 7 giờ (420 phút) vào tổng thời gian
            totalMinutes += 420;

            // Đảm bảo thời gian trong phạm vi 24 giờ (1440 phút)
            if (totalMinutes >= 1440f)
            {
                totalMinutes -= 1440f;
            }

            int hours = Mathf.FloorToInt(totalMinutes / 60f); // Tính giờ
            int minutes = Mathf.FloorToInt(totalMinutes % 60f); // Tính phút

            // Hiển thị thời gian dưới dạng HH:mm
            clockText.text = $"{hours:00}:{minutes:00}";
        }
    }
}
