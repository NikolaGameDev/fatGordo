using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject[] obstacles; // Obstacle prefabs
    public GameObject[] collectables; // Collectable prefabs

    [Header("Spawn Settings")]
    public Transform spawnPoint; // Fixed spawn position
    public float minSpawnTime = 1f; // Minimum time between spawns
    public float maxSpawnTime = 6f; // Maximum time between spawns
    public float minimumDistance = 3f; // Minimum distance (in time) between objects

    [Header("Break Settings")]
    public float breakDuration = 5f; // Duration of the relax zone
    public float breakInterval = 45f; // Time between breaks

    [Header("Speed Settings")]
    public float baseSpeed = 5f; // Initial speed of spawned objects
    public float speedIncrement = 1f; // How much speed increases per interval
    public float maxSpeed = 20f; // Maximum speed cap
    public float speedIncreaseInterval = 10f; // Time between speed increases
    public float offScreenX = -15f; // X position for destroying objects

    private float nextSpawnTime; // Time to spawn the next object
    private float nextSpeedIncreaseTime; // Time to increase object speed
    private float nextBreakTime; // Time for the next relax zone
    private float currentSpeed; // Current movement speed of objects
    private bool isInBreak; // Whether the game is in a relax zone
    private int consecutiveObstacles; // Tracks consecutive obstacle spawns

    private void Start()
    {
        currentSpeed = baseSpeed;
        nextSpeedIncreaseTime = Time.time + speedIncreaseInterval;
        nextBreakTime = Time.time + breakInterval;
        nextSpawnTime = Time.time + Random.Range(minSpawnTime, maxSpawnTime);
    }

    private void Update()
    {
        HandleSpeedIncrease();
        HandleBreaks();

        if (!isInBreak)
        {
            HandleSpawning();
        }
    }

    private void HandleSpeedIncrease()
    {
        if (Time.time >= nextSpeedIncreaseTime)
        {
            currentSpeed = Mathf.Min(maxSpeed, currentSpeed + speedIncrement);
            nextSpeedIncreaseTime = Time.time + speedIncreaseInterval;
        }
    }

    private void HandleBreaks()
    {
        // Trigger a relax zone at the set interval
        if (!isInBreak && Time.time >= nextBreakTime)
        {
            isInBreak = true;
            Debug.Log("Entering Relax Zone!");
            nextSpawnTime = Time.time + breakDuration; // Delay spawning during the relax zone
        }

        // Exit the relax zone after the break duration
        if (isInBreak && Time.time >= nextBreakTime + breakDuration)
        {
            isInBreak = false;
            nextBreakTime = Time.time + breakInterval + breakDuration; // Schedule the next break
            Debug.Log("Exiting Relax Zone!");
        }
    }

    private void HandleSpawning()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnObject();

            // Randomize next spawn time within the range, considering minimum distance
            float spawnInterval = Random.Range(minSpawnTime, maxSpawnTime);
            nextSpawnTime = Time.time + Mathf.Max(spawnInterval, minimumDistance / currentSpeed);
        }
    }

    private void SpawnObject()
    {
        GameObject[] pool;

        // Ensure balanced spawns between obstacles and collectables
        if (consecutiveObstacles >= 3 || Random.value > 0.5f)
        {
            pool = collectables;
            consecutiveObstacles = 0;
        }
        else
        {
            pool = obstacles;
            consecutiveObstacles++;
        }

        // Spawn randomly selected prefab
        GameObject prefabToSpawn = pool[Random.Range(0, pool.Length)];
        GameObject spawnedObject = Instantiate(prefabToSpawn, spawnPoint.position, Quaternion.identity);

        // Move the spawned object
        MoveObject(spawnedObject);
    }

    private void MoveObject(GameObject obj)
    {
        obj.AddComponent<MoveHandler>().Initialize(currentSpeed, offScreenX);
    }

    // Internal class to handle object movement
    private class MoveHandler : MonoBehaviour
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
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);

            // Destroy object when it moves off-screen
            if (transform.position.x < destroyX)
            {
                Destroy(gameObject);
            }
        }
    }
}
