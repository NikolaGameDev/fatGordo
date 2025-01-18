using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int score;

    public void AddScore(int amount)
    {
        score += amount;
        scoreText.text = "SCORE : "+score;
    }
}
