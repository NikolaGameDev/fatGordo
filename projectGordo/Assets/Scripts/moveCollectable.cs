using UnityEngine;
using UnityEngine.SceneManagement;

public class Collectable : MonoBehaviour
{
   // public float speed;
    public int scoreValue = 10;
    private gameManager gameManager;
    private void Start()
    {
        gameManager = Object.FindAnyObjectByType<gameManager>();  
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collectable collides with the player
        if (other.CompareTag("Player"))  // Assuming the player has a "Player" tag
        {
            gameManager.playCollectSound();
            Debug.Log("Collectable collected!");
            Object.FindAnyObjectByType<ScoreManager>()?.AddScore(scoreValue);
            Destroy(gameObject);  // Optional: Destroy the collectable after being collected
        }
    }
}
