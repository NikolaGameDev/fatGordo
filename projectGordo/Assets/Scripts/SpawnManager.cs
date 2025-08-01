using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    [System.Serializable]
    public class SpawnPhase
    {
        public float moveSpeed = 6f;          // world/hazard speed for this phase
        public float duration = 30f;          // seconds
        public float minDelayMultiplier = 0.9f;
        public float maxDelayMultiplier = 1.5f;
    }

    [Header("Prefabs")]
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private GameObject collectablePrefab;

    [Header("Spawn Settings")]
    [SerializeField] private float minDistance = 2.5f;        // spacing baseline (world units)
    [SerializeField] private float destroyAfterSeconds = 10f; // lifetime
    [SerializeField] private float breakBetweenPhases = 2f;   // pause between phases

    [Header("Phases")]
    [SerializeField] private List<SpawnPhase> phases = new List<SpawnPhase>();

    // runtime
    private readonly List<Transform> active = new List<Transform>();
    private float currentSpeed = 0f;

    void Start()
    {
        StartCoroutine(PhaseManager());
    }

    void Update()
    {
        // Centralized movement (no Rigidbody2D on spawned objects)
        if (active.Count == 0) return;

        float dx = currentSpeed * Time.deltaTime;
        for (int i = active.Count - 1; i >= 0; i--)
        {
            Transform t = active[i];
            if (t == null) { active.RemoveAt(i); continue; }
            t.Translate(Vector3.left * dx, Space.World);
        }
    }

    IEnumerator PhaseManager()
    {
        for (int i = 0; i < phases.Count; i++)
        {
            var phase = phases[i];
            currentSpeed = phase.moveSpeed;
            Debug.Log($"Starting Phase {i + 1} | speed {currentSpeed}");

            yield return StartCoroutine(SpawnLoop(phase));

            if (i < phases.Count - 1)
            {
                Debug.Log("Break between phases…");
                yield return new WaitForSeconds(breakBetweenPhases);
            }
        }

        // Loop last phase forever
        if (phases.Count > 0)
        {
            var last = phases[phases.Count - 1];
            while (true)
            {
                currentSpeed = last.moveSpeed;
                yield return StartCoroutine(SpawnLoop(last));
            }
        }
    }

    IEnumerator SpawnLoop(SpawnPhase phase)
    {
        float elapsed = 0f;

        while (elapsed < phase.duration)
        {
            // choose prefab and spawn at spawner position
            GameObject prefab = (Random.value > 0.5f) ? collectablePrefab : obstaclePrefab;
            GameObject obj = Instantiate(prefab, transform.position, Quaternion.identity);

            // track & schedule destroy
            var tr = obj.transform;
            active.Add(tr);
            if (destroyAfterSeconds > 0f) StartCoroutine(DestroyAfter(obj, destroyAfterSeconds));

            // spacing delay based on current speed (respects phase speed)
            float baseDelay = minDistance / phase.moveSpeed;
            float delay = baseDelay * Random.Range(phase.minDelayMultiplier, phase.maxDelayMultiplier);

            yield return new WaitForSeconds(delay);
            elapsed += delay;
        }
    }

    IEnumerator DestroyAfter(GameObject go, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (go != null)
        {
            active.Remove(go.transform);
            Destroy(go);
        }
    }
}
