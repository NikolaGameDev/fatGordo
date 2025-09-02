using UnityEngine;

public class move : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;
    public bool destroyOffScreen = true;
    public float destroyX = -15f;

    void Update()
    {
        // Only move on the X axis (2D style)
        transform.position = new Vector2(
            transform.position.x - speed * Time.deltaTime,
            transform.position.y
        );

        if (destroyOffScreen && transform.position.x < destroyX)
        {
            Destroy(gameObject);
        }
    }
}
