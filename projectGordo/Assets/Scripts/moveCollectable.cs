using UnityEngine;
using UnityEngine.SceneManagement;

public class Collectable : MonoBehaviour
{
    public int scoreValue = 10;
    private gameManager gameManager;

    private void Start()
    {
        gameManager = Object.FindAnyObjectByType<gameManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ScoreManager scoreManager = Object.FindFirstObjectByType<ScoreManager>();

            scoreManager.AddScore(scoreValue); // Just update the score
            gameManager.playCollectSound();
            Object.FindFirstObjectByType<popGlow>()?.TriggerCollectableEffect();
            Debug.Log("Collectable collected!");
            Destroy(gameObject);
        }
    }
}
