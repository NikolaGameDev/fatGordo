using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject[] clusterPrefabs;
    public Transform spawnPoint;

    [Header("Spawn Timing")]
    public float initialSpawnInterval = 3f;
    public float minSpawnInterval = 1f;
    public float spawnIntervalReduction = 0.05f;
    public float spawnAdjustmentRate = 30f;

    [Header("Speed Settings")]
    public float initialSpeed = 5f;
    public float maxSpeed = 30f;
    public float speedIncreaseAmount = 2f;
    public float speedIncreaseInterval = 60f;

    [Header("Destroy Settings")]
    public float destroyX = -15f; // X position where objects are destroyed

    private float currentSpawnInterval;
    private float nextSpawnTime;
    private float currentSpeed;
    private float nextSpeedIncreaseTime;
    private float nextSpawnAdjustmentTime;

    private void Start()
    {
        currentSpawnInterval = initialSpawnInterval;
        currentSpeed = initialSpeed;
        float gameTime = Time.timeSinceLevelLoad;
        nextSpawnTime = gameTime + currentSpawnInterval;
        nextSpeedIncreaseTime = gameTime + speedIncreaseInterval;
        nextSpawnAdjustmentTime = gameTime + spawnAdjustmentRate;
    }

    private void Update()
    {
        float gameTime = Time.timeSinceLevelLoad;

        if (gameTime >= nextSpawnTime)
        {
            SpawnCluster();
            nextSpawnTime = gameTime + currentSpawnInterval;
        }

        if (gameTime >= nextSpeedIncreaseTime)
        {
            IncreaseSpeed();
        }

        if (gameTime >= nextSpawnAdjustmentTime)
        {
            AdjustSpawnRate();
        }
    }

    private void IncreaseSpeed()
    {
        if (currentSpeed >= maxSpeed) return;

        currentSpeed = Mathf.Min(maxSpeed, currentSpeed + speedIncreaseAmount);
        nextSpeedIncreaseTime += speedIncreaseInterval;
    }

    private void AdjustSpawnRate()
    {
        if (currentSpawnInterval <= minSpawnInterval) return;

        currentSpawnInterval = Mathf.Max(minSpawnInterval, currentSpawnInterval - spawnIntervalReduction);
        nextSpawnAdjustmentTime += spawnAdjustmentRate;
    }

    private void SpawnCluster()
    {
        if (clusterPrefabs.Length == 0)
        {
            Debug.LogWarning("Spawner: No cluster prefabs assigned.");
            return;
        }

        GameObject spawnedCluster = Instantiate(clusterPrefabs[Random.Range(0, clusterPrefabs.Length)], spawnPoint.position, Quaternion.identity);
        ApplyMovementToCluster(spawnedCluster.transform);
    }

    private void ApplyMovementToCluster(Transform cluster)
    {
        foreach (Transform child in cluster)
        {
            child.gameObject.AddComponent<MoveObject>().Initialize(currentSpeed, destroyX);
        }
    }

    private class MoveObject : MonoBehaviour
    {
        private float moveSpeed;
        private float destroyX;

        public void Initialize(float speed, float offScreenX)
        {
            moveSpeed = speed;
            destroyX = offScreenX;
        }

        private void Update()
        {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;

            if (transform.position.x < destroyX)
            {
                Destroy(gameObject);
            }
        }
    }
}
