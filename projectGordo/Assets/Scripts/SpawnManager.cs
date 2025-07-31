using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    [System.Serializable]
    public class SpawnPhase
    {
        public float moveSpeed;
        public float duration;
        public float minDelayMultiplier;
        public float maxDelayMultiplier;
    }

    [Header("Prefabs")]
    public GameObject obstaclePrefab;
    public GameObject collectablePrefab;

    [Header("Spawn Settings")]
    public float minDistance;
    public float destroyAfterSeconds;
    public float breakBetweenPhases;

    [Header("Phases")]
    public List<SpawnPhase> phases = new List<SpawnPhase>();

    void Start()
    {
        StartCoroutine(PhaseManager());
    }

    IEnumerator PhaseManager()
    {
        foreach (var phase in phases)
        {
            Debug.Log($"Starting Phase with speed {phase.moveSpeed}");

            yield return StartCoroutine(SpawnLoop(phase));

            Debug.Log("Break between phases...");
            yield return new WaitForSeconds(breakBetweenPhases);
        }

        Debug.Log("All phases complete. Looping last phase forever.");
        // Infinite loop on last phase
        if (phases.Count > 0)
            while (true)
                yield return StartCoroutine(SpawnLoop(phases[phases.Count - 1]));
    }

    IEnumerator SpawnLoop(SpawnPhase phase)
    {
        float elapsed = 0f;

        while (elapsed < phase.duration)
        {
            GameObject prefab = Random.value > 0.5f ? collectablePrefab : obstaclePrefab;
            GameObject obj = Instantiate(prefab, transform.position, Quaternion.identity);

            if (obj.TryGetComponent<Rigidbody2D>(out var rb))
                rb.linearVelocity = Vector2.left * phase.moveSpeed;

            Destroy(obj, destroyAfterSeconds);

            float baseDelay = minDistance / phase.moveSpeed;
            float delay = baseDelay * Random.Range(phase.minDelayMultiplier, phase.maxDelayMultiplier);

            yield return new WaitForSeconds(delay);
            elapsed += delay;
        }
    }
}
