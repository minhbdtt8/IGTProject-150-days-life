using System.Collections;
using UnityEngine;

public class FishingRod : MonoBehaviour
{
    public Transform anchorPoint; // Điểm neo (anchor point) của cần câu
    public GameObject hook;       // Lưỡi câu (hook)
    public float castDistance = 5f; // Khoảng cách thả lưỡi câu
    public bool isFishing = false;  // Trạng thái đang câu cá

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isFishing)
        {
            StartCoroutine(CastLine());
        }
    }

    IEnumerator CastLine()
    {
        isFishing = true;
        Vector3 targetPosition = anchorPoint.position + Vector3.down * castDistance;
        float time = 0;

        // Thả lưỡi câu xuống
        while (time < 1)
        {
            hook.transform.position = Vector3.Lerp(anchorPoint.position, targetPosition, time);
            time += Time.deltaTime;
            yield return null;
        }

        // Đợi 10 giây để có khả năng câu cá
        yield return new WaitForSeconds(60f);

        // Thu lưỡi câu lên
        time = 0;
        while (time < 1)
        {
            hook.transform.position = Vector3.Lerp(targetPosition, anchorPoint.position, time);
            time += Time.deltaTime;
            yield return null;
        }

        isFishing = false;
    }
}
