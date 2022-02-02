using System;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public int PlayerScore { get; private set; } = 0;
    private int HighScore = 0;
    private int NewBest = 0;
    public GameObject ScoreTMP;
    public GameObject HighScoreTMP;

    private TMP_Text scoreText;
    private TMP_Text highScoreText;


    private void Awake()
    {
        scoreText = ScoreTMP.GetComponent<TMP_Text>();
        highScoreText = HighScoreTMP.GetComponent<TMP_Text>();
    }

    public void Update()
    {
        scoreText.text = $"Score: {PlayerScore}";

        if (PlayerScore > HighScore)
        {
            HighScore = PlayerScore;
        }
        
        if (HighScore > NewBest)
        {
            highScoreText.text = $"Best: {HighScore}";
        }
        else
        {
            highScoreText.text = $"Best: {NewBest}";
        }
    }

    public void AddScore()
    {
        PlayerScore++;
        Debug.Log(PlayerScore);
    }

    public void ResetScore()
    {
        if (HighScore > NewBest)
        {
            HighScore = NewBest;
        }
        PlayerScore = 0;
    }
}
