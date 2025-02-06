using TMPro;
using UnityEngine;
using System.Linq;

public class ScoreManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI[] highScoreTexts; // UI elements for high scores (Top 5)

    private int score;
    private int[] highScores = new int[5]; // Array to store top 5 scores

    private void Start()
    {
        LoadHighScores();
        UpdateHighScoreUI();
    }

    public void AddScore(int amount)
    {
        score += amount;
        scoreText.text = "SCORE: " + score;
    }

    public void CheckForHighScore()
    {
        // Add current score, sort, and keep top 5
        highScores = highScores.Append(score).OrderByDescending(s => s).Take(5).ToArray();
        SaveHighScores();
        UpdateHighScoreUI();
    }

    private void SaveHighScores()
    {
        for (int i = 0; i < highScores.Length; i++)
        {
            PlayerPrefs.SetInt("HighScore" + i, highScores[i]);
        }
        PlayerPrefs.Save();
    }

    private void LoadHighScores()
    {
        for (int i = 0; i < highScores.Length; i++)
        {
            highScores[i] = PlayerPrefs.GetInt("HighScore" + i, 0);
        }
    }

    private void UpdateHighScoreUI()
    {
        for (int i = 0; i < highScores.Length; i++)
        {
            if (highScoreTexts != null && i < highScoreTexts.Length)
            {
                highScoreTexts[i].text = $"{i + 1}. {highScores[i]}";
            }
        }
    }

    public int GetCurrentScore()
    {
        return score;
    }
    public void ResetHighScores()
    {
        Debug.Log("Reset Button Clicked!"); // Check if this message appears in Console
        for (int i = 0; i < highScores.Length; i++)
        {
            PlayerPrefs.SetInt("HighScore" + i, 0);
            highScores[i] = 0;
        }
        PlayerPrefs.Save();
        UpdateHighScoreUI();
        Debug.Log("High scores reset!");
    }

}
