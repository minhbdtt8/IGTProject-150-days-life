using UnityEngine;

public class inventory : MonoBehaviour
{
    public GameObject objectToSpawn;
    public Transform attachPoint;

    private GameObject spawnedObject;

    // Biến trạng thái đang cầm cần câu
    public bool isHoldingRod { get; private set; } = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SpawnAndAttachObject();
        }

        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            DestroyObject();
        }
    }

    void SpawnAndAttachObject()
    {
        if (spawnedObject == null)
        {
            spawnedObject = Instantiate(objectToSpawn, attachPoint.position, attachPoint.rotation);
            spawnedObject.transform.SetParent(attachPoint);
            isHoldingRod = true; // Đã cầm cần
        }
    }

    void DestroyObject()
    {
        if (spawnedObject != null)
        {
            Destroy(spawnedObject);
            spawnedObject = null;
            isHoldingRod = false; // Bỏ cần
        }
    }
}
