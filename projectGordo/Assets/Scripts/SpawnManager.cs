using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    [System.Serializable]
    public class SpawnPhase
    {
        public float moveSpeed = 6f;          // speed for this phase
        public float duration = 30f;          // seconds
        public float minDelayMultiplier = 0.9f;
        public float maxDelayMultiplier = 1.5f;
    }

    // Expose current speed for other systems (e.g., Gordo animator)
    public static float CurrentSpeed { get; private set; } = 0f;

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

    void Start()
    {
        StartCoroutine(PhaseManager());
    }

    void Update()
    {
        if (active.Count == 0) return;

        float dx = CurrentSpeed * Time.deltaTime;
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

            // publish speed
            CurrentSpeed = phase.moveSpeed;
            Debug.Log($"Starting Phase {i + 1} | speed {CurrentSpeed}");

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
                CurrentSpeed = last.moveSpeed;
                yield return StartCoroutine(SpawnLoop(last));
            }
        }
    }

    IEnumerator SpawnLoop(SpawnPhase phase)
    {
        float elapsed = 0f;

        // sanity: ensure min<=max for delay multipliers
        float minMul = Mathf.Min(phase.minDelayMultiplier, phase.maxDelayMultiplier);
        float maxMul = Mathf.Max(phase.minDelayMultiplier, phase.maxDelayMultiplier);

        // avoid division by zero
        float speed = Mathf.Max(phase.moveSpeed, 0.01f);

        while (elapsed < phase.duration)
        {
            // pick prefab and spawn at spawner position
            GameObject prefab = (Random.value > 0.5f) ? collectablePrefab : obstaclePrefab;
            GameObject obj = Instantiate(prefab, transform.position, Quaternion.identity);

            // track & schedule destroy
            var tr = obj.transform;
            active.Add(tr);
            if (destroyAfterSeconds > 0f) StartCoroutine(DestroyAfter(obj, destroyAfterSeconds));

            // spacing-based delay with randomness
            float baseDelay = minDistance / speed;
            float delay = baseDelay * Random.Range(minMul, maxMul);

            yield return new WaitForSeconds(delay);
            elapsed += delay;
        }
    }

    IEnumerator DestroyAfter(GameObject go, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (go != null)
        {
            var tr = go.transform;
            int idx = active.IndexOf(tr);
            if (idx >= 0) active.RemoveAt(idx);
            Destroy(go);
        }
    }
}
