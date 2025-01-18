using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public GameObject obstaclePrefab;  // Obstacle prefab
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
        // Increment timer and spawn obstacle when time is up
        if ((spawnTimer += Time.deltaTime) >= spawnRate)
        {
            SpawnObstacle();
            spawnTimer = 0f;  // Reset timer
            SetRandomSpawnRate();  // Set next random spawn rate
        }
    }

    private void SetRandomSpawnRate()
    {
        // Randomize the spawn rate between min and max values
        spawnRate = Random.Range(minSpawnRate, maxSpawnRate);
    }

    private void SpawnObstacle()
    {
        // Spawn all obstacles from the same position as the ObstacleManager
        Vector2 spawnPosition = new Vector2(transform.position.x, transform.position.y);
        GameObject obstacle = Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
    }
}
