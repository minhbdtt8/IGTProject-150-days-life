using UnityEngine;

public class PayObject : MonoBehaviour
{
    public int cost; // Số tiền sẽ bị trừ khi player chạm vào

    void Start()
    {
        // Bạn có thể đặt số tiền trực tiếp trong Inspector cho từng đối tượng
        // Hoặc khởi tạo giá trị mặc định nếu muốn
        if (cost <= 0) cost = 10; // Ví dụ: nếu không đặt, sẽ trừ mặc định là 10
    }
}
