using UnityEngine;

public class PlayerController1 : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GameManager1.Instance.SpawnUnit(1, true); // Triệu hồi Tam Giác
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            GameManager1.Instance.SpawnUnit(2, true); // Triệu hồi Vuông
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            GameManager1.Instance.SpawnUnit(3, true); // Triệu hồi Lục Giác
        }
    }
}
