using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class moveObstacle : MonoBehaviour
{
    //public float speed;
   // private bool isGameOver = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the obstacle collides with the player
        if (other.CompareTag("Player"))  // Assuming the player has a "Player" tag
        {
            gameManager manager = Object.FindFirstObjectByType<gameManager>(); // Find the GameManager in the scene
            Debug.Log("Obstacle hit the player!");
            Destroy(gameObject);  // Optional: Destroy the obstacle after hitting the player
            manager.ShowGameOverUI();
        }
    }
    
}
