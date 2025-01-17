using UnityEngine;

public class moveObstacle : MonoBehaviour
{
    private float speed;

    // Initialize the obstacle with a speed
    public void Initialize(float obstacleSpeed)
    {
        speed = obstacleSpeed;
    }

    private void Update()
    {
        // Move the obstacle towards the player (move left in 2D)
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        // Destroy the obstacle when it goes off-screen (adjust the value as necessary)
        if (transform.position.x < -10f)  // Adjust based on your camera view
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the obstacle collides with the player
        if (other.CompareTag("Player"))  // Assuming the player has a "Player" tag
        {
            Debug.Log("Obstacle hit the player!");
            Destroy(gameObject);  // Optional: Destroy the obstacle after hitting the player
        }
    }
}
