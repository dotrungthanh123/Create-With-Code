using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    private TextMeshProUGUI scoreText;
    public int playerScore = 0;
    public int highScore;

    private void Start() {
        scoreText = GetComponent<TextMeshProUGUI>();
        UpdateScore();
    }

    public void IncreaseScore(int increment) {
        playerScore += increment;
        UpdateScore();
    }

    private void UpdateScore() {
        scoreText.text = "Score: " + playerScore;
    }
    
}
