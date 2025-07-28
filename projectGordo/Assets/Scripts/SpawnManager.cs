using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour
{
    [Header("Spawn Prefabs")]
    public GameObject obstaclePrefab;
    public GameObject collectablePrefab;

    [Header("Spawn Behavior")]
    public float minDistance = 2.5f;               // Minimum spacing in world units
    public float moveSpeed = 6f;                   // How fast objects move
    public float destroyAfterSeconds = 10f;        // Lifespan of spawned object

    [Header("Spawn Rate Randomness")]
    [Range(0.8f, 2f)] public float minSpawnRateMultiplier = 1.0f;
    [Range(0.8f, 2f)] public float maxSpawnRateMultiplier = 1.5f;

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            // Spawn randomly chosen prefab
            GameObject prefab = Random.value > 0.5f ? collectablePrefab : obstaclePrefab;
            GameObject obj = Instantiate(prefab, transform.position, Quaternion.identity);

            if (obj.TryGetComponent<Rigidbody2D>(out var rb))
                rb.velocity = Vector2.left * moveSpeed;

            Destroy(obj, destroyAfterSeconds);

            // Base delay from speed and distance
            float baseDelay = minDistance / moveSpeed;

            // Randomized multiplier for spawn rate variation
            float multiplier = Random.Range(minSpawnRateMultiplier, maxSpawnRateMultiplier);
            float finalDelay = baseDelay * multiplier;

            yield return new WaitForSeconds(finalDelay);
        }
    }
}
