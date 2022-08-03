using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public static ScoreController instance;
    private void Awake()
    {
        instance = this;
    }
    private int score;
    private int totalRowsClear;

    private int oneRowCleared = 10;
    private int twoRowCleared = 50;
    private int threeRowCleared = 100;
    private int fourRowCleared = 400;

    public void UpdateScore(int rowCleared)
    {
        switch (rowCleared)
        {
            case 1:
                updateScore(oneRowCleared);
                break;
            case 2:
                updateScore(twoRowCleared);
                break;
            case 3:
                updateScore(threeRowCleared);
                break;
            case 4:
                updateScore(fourRowCleared);
                break;
        }
        totalRowsClear += rowCleared;
        updateHighScore();
    }

    private void updateScore(int value)
    {
        score += value;
    }

    public int getScore()
    {
        return score;
    }

    public int getTotalRowsClear()
    {
        return totalRowsClear;
    }

    public void updateHighScore()
    {
        if (score > PlayerPrefs.GetInt("HighScore1"))
        {
            PlayerPrefs.SetInt("HighScore3", PlayerPrefs.GetInt("HighScore2"));
            PlayerPrefs.SetInt("HighScore2", PlayerPrefs.GetInt("HighScore1"));
            PlayerPrefs.SetInt("HighScore1", score);
        }
        else if (score > PlayerPrefs.GetInt("HighScore2") && score < PlayerPrefs.GetInt("HighScore1"))
        {
            PlayerPrefs.SetInt("HighScore3", PlayerPrefs.GetInt("HighScore2"));
            PlayerPrefs.SetInt("HighScore2", score);
        }
        else
        {
            PlayerPrefs.SetInt("HighScore3", score);
        }
    }
}
