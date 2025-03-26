using UnityEngine;

public class PipeMove : MonoBehaviour
{
    public float speed = 2f;

    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;

        if (transform.position.x < -10f) // Nếu ống khói ra khỏi màn hình
        {
            Destroy(gameObject);
        }
    }
}
