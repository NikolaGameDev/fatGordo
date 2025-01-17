using UnityEngine;
using UnityEngine.SceneManagement;

public class Collectable : MonoBehaviour
{
    private float speed;
    public int scoreValue = 10;

    // Initialize the collectable with a speed
    public void Initialize(float collectableSpeed)
    {
        speed = collectableSpeed;
    }

    private void Update()
    {
        // Move the collectable towards the player (move left in 2D)
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        // Destroy the collectable when it goes off-screen
        if (transform.position.x < -10f)  // Adjust based on your camera view
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collectable collides with the player
        if (other.CompareTag("Player"))  // Assuming the player has a "Player" tag
        {
            Debug.Log("Collectable collected!");
            Object.FindAnyObjectByType<ScoreManager>()?.AddScore(scoreValue);
            Destroy(gameObject);  // Optional: Destroy the collectable after being collected
        }
    }
}
