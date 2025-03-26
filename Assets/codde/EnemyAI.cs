using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    public float minFlipDelay = 1f; // Độ trễ tối thiểu
    public float maxFlipDelay = 3f; // Độ trễ tối đa
    private bool isLookingRight = true;

    void Start()
    {
        StartCoroutine(FlipRandomly());
    }

    IEnumerator FlipRandomly()
    {
        while (true)
        {
            float delay = Random.Range(minFlipDelay, maxFlipDelay); // Chọn ngẫu nhiên thời gian lật
            yield return new WaitForSeconds(delay);
            Flip();
        }
    }

    private void Flip()
    {
        isLookingRight = !isLookingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}