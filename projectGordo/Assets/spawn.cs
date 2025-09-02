using UnityEngine;

public class spawn : MonoBehaviour
{
    [Header("Spawner Settings")]
    public GameObject[] prefabs;     // 3 prefabs you want to spawn
    public float spawnInterval = 2f; // Seconds between spawns
    public Vector2 spawnPosition = new Vector2(10f, 0f); // Where to spawn (X=right side)
    public Vector2 randomYRange = new Vector2(-2f, 2f);  // Optional random Y offset

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            Spawn();
            timer = 0f;
        }
    }

    void Spawn()
    {
        if (prefabs.Length == 0) return;

        // Pick a random prefab from the array
        int index = Random.Range(0, prefabs.Length);

        // Randomize Y position a bit (optional)
        float randomY = Random.Range(randomYRange.x, randomYRange.y);

        // Spawn
        Instantiate(prefabs[index], new Vector2(spawnPosition.x, spawnPosition.y + randomY), Quaternion.identity);
    }
}
