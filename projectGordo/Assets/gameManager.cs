using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    public GameObject gameOverUI; // Reference to the Game Over UI
    public Button restartButton; // Reference to the restart button
    public Button quitButton; // Reference to the quit button


    private void Start()
    {
        restartButton.onClick.AddListener(RestartLevel);
        quitButton.onClick.AddListener(QuitGame);
    }
    public void ShowGameOverUI()
    {
        gameOverUI.SetActive(true); // Activate Game Over UI
        Time.timeScale = 0f; // Freeze the game
    }
    public void RestartLevel()
    {
        Time.timeScale = 1f; // Unpause the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Restart the current scene
    }

    public void QuitGame()
    {
        Time.timeScale = 1f; // Unpause the game before quitting
        Application.Quit(); // Quit the application
    }
}
