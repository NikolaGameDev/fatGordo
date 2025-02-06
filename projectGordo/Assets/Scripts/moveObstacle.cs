using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class moveObstacle : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager manager = Object.FindFirstObjectByType<gameManager>();
            ScoreManager scoreManager = FindFirstObjectByType<ScoreManager>();

            Debug.Log("Obstacle hit the player!");

            scoreManager.AddScore(0); // Force UI to refresh before checking high score
            scoreManager.CheckForHighScore(); // Now saves the correct final score

            manager.ShowGameOverUI();
            Time.timeScale = 0; // Stop the game
        }
    }
}
