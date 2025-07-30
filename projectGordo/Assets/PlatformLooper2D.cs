using UnityEngine;
using System.Collections.Generic;

public class PlatformLooper2D : MonoBehaviour
{
    [Header("Platform Setup")]
    [SerializeField] private GameObject[] platformPrefabs;
    [SerializeField] private float platformSpeed = 5f;
   // [SerializeField] private int maxPlatforms = 3;

    private List<GameObject> activePlatforms = new List<GameObject>();

    void Start()
    {
        // Spawn first platform at starting point
        SpawnPlatform(Vector3.zero);
    }

    void Update()
    {
        float moveAmount = platformSpeed * Time.deltaTime;

        for (int i = activePlatforms.Count - 1; i >= 0; i--)
        {
            GameObject plat = activePlatforms[i];
            plat.transform.position += Vector3.left * moveAmount;

            if (plat.transform.position.x < -30f) // off-screen cleanup
            {
                Destroy(plat);
                activePlatforms.RemoveAt(i);
            }
        }
    }

    public void SpawnNextPlatform()
    {
        if (activePlatforms.Count == 0) return;

        GameObject lastPlat = activePlatforms[activePlatforms.Count - 1];
        float newX = lastPlat.transform.position.x + GetPlatformWidth(lastPlat);

        SpawnPlatform(new Vector3(newX, lastPlat.transform.position.y, 0f));
    }

    private void SpawnPlatform(Vector3 pos)
    {
        int index = Random.Range(0, platformPrefabs.Length);
        GameObject newPlat = Instantiate(platformPrefabs[index], pos, Quaternion.identity);
        activePlatforms.Add(newPlat);
    }

    private float GetPlatformWidth(GameObject platform)
    {
        SpriteRenderer sr = platform.GetComponentInChildren<SpriteRenderer>();
        return sr ? sr.bounds.size.x : 20f;
    }

    public void SetMoveSpeed(float speed)
    {
        platformSpeed = speed;
    }
}
