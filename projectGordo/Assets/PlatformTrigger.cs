using UnityEngine;

public class PlatformTrigger : MonoBehaviour
{
    [SerializeField] private GameObject[] platformPrefabs;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float platformSpeed = 5f;

    [Header("Debug")]
    public bool spawnManually = false;

    private bool triggered = false;

    private void Update()
    {
        if (spawnManually)
        {
           spawnManually = false; // Reset it so it doesn’t spam
            SpawnNextPlatform();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered || !other.CompareTag("Player")) return;
        triggered = true;
        SpawnNextPlatform();
    }

    private void SpawnNextPlatform()
    {
        int index = Random.Range(0, platformPrefabs.Length);
        GameObject newPlat = Instantiate(platformPrefabs[index], spawnPoint.position, Quaternion.identity);

        ScrollingPlatform scroll = newPlat.GetComponent<ScrollingPlatform>();
        if (scroll != null)
            scroll.SetSpeed(platformSpeed);

    }
}
