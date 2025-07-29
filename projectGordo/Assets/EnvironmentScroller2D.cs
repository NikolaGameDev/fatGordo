using UnityEngine;

public class EnvironmentScroller2D : MonoBehaviour
{
    [Header("Parallax Layers")]
    [SerializeField] private Transform treesLayer;
    [SerializeField] private float treesSpeed = 3f;

    [SerializeField] private Transform mountain1;
    [SerializeField] private float mountain1Speed = 1.5f;

    [SerializeField] private Transform mountain2;
    [SerializeField] private float mountain2Speed = 1f;

    [SerializeField] private Transform mountain3;
    [SerializeField] private float mountain3Speed = 0.5f;

    [Header("Platform Movement")]
    [SerializeField] private Transform[] platforms;
    [SerializeField] private float platformSpeed = 5f;

    public void SetPlatformSpeed(float speed)
    {
        platformSpeed = speed;
    }

    void Update()
    {
        float delta = Time.deltaTime;

        // Parallax background movement
        MoveLayer(treesLayer, treesSpeed * delta);
        MoveLayer(mountain1, mountain1Speed * delta);
        MoveLayer(mountain2, mountain2Speed * delta);
        MoveLayer(mountain3, mountain3Speed * delta);

        // Move platforms
        foreach (Transform platform in platforms)
        {
            if (platform != null)
                platform.position += Vector3.left * platformSpeed * delta;
        }
    }

    private void MoveLayer(Transform layer, float amount)
    {
        if (layer != null)
            layer.position += Vector3.left * amount;
    }
}
