using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    public GameObject pipePrefab;
    public float spawnRate = 2f;
    public float heightOffset = 1f;

    void Start()
    {
        InvokeRepeating("SpawnPipe", 1f, spawnRate);
    }

    void SpawnPipe()
    {
        float randomY = Random.Range(-heightOffset, heightOffset);
        Vector3 spawnPosition = new Vector3(5, randomY, 0);
        Instantiate(pipePrefab, spawnPosition, Quaternion.identity);
    }
}
