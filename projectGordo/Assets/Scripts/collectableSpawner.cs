using UnityEngine;

public class collectableSpawner : MonoBehaviour
{
    public GameObject collectablePrefab;  // Collectable prefab
    public float minSpawnRate = 1f;  // Minimum time between spawns
    public float maxSpawnRate = 5f;  // Maximum time between spawns

    private float spawnRate;  // Current spawn rate
    private float spawnTimer;  // Timer to track spawn intervals

    private void Start()
    {
        SetRandomSpawnRate();  // Initialize random spawn rate
    }

    private void Update()
    {
        // Increment timer and spawn collectable when time is up
        if ((spawnTimer += Time.deltaTime) >= spawnRate)
        {
            SpawnCollectable();
            spawnTimer = 0f;  // Reset timer
            SetRandomSpawnRate();  // Set next random spawn rate
        }
    }

    private void SetRandomSpawnRate()
    {
        // Randomize the spawn rate between min and max values
        spawnRate = Random.Range(minSpawnRate, maxSpawnRate);
    }

    private void SpawnCollectable()
    {
        // Spawn all collectables from the same position as the CollectableManager
        Vector2 spawnPosition = new Vector2(transform.position.x, transform.position.y);
        GameObject collectable = Instantiate(collectablePrefab, spawnPosition, Quaternion.identity);
    }
}
