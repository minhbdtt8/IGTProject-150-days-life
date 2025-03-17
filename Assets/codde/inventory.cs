using UnityEngine;

public class inventory : MonoBehaviour
{
    public GameObject objectToSpawn;

    // Vị trí gắn liền với nhân vật (có thể là một vị trí cụ thể trên nhân vật)
    public Transform attachPoint;

    // Biến để lưu trữ tham chiếu đến object đã spawn
    private GameObject spawnedObject;

    void Update()
    {
        // Kiểm tra nếu người chơi nhấn phím `1` để spawn đồ vật
        if (Input.GetKeyDown(KeyCode.Alpha1)) // Thay Alpha1 bằng phím mong muốn
        {
            SpawnAndAttachObject();
        }

        // Kiểm tra nếu người chơi nhấn phím ` để hủy đồ vật
        if (Input.GetKeyDown(KeyCode.BackQuote)) // BackQuote là phím `
        {
            DestroyObject();
        }
    }

    void SpawnAndAttachObject()
    {
        // Kiểm tra nếu đã có object được spawn, tránh spawn nhiều lần
        if (spawnedObject == null)
        {
            // Spawn object và gắn vào vị trí attachPoint
            spawnedObject = Instantiate(objectToSpawn, attachPoint.position, attachPoint.rotation);

            // Gắn object vào attachPoint
            spawnedObject.transform.SetParent(attachPoint);
        }
    }

    void DestroyObject()
    {
        // Kiểm tra nếu object đã tồn tại, thì hủy nó
        if (spawnedObject != null)
        {
            Destroy(spawnedObject);
            spawnedObject = null;
        }
    }
}

