using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Display the Score , Active Level and Number of Rows Cleared
/// </summary>
public class DisplayController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI rowsClearedText;

    [SerializeField] private ScoreController scoreController;

    void Start()
    {
        displayScore();
        displayLevel();
        displayRowsCleared();
    }

    public void UpdateDisplay()
    {
        displayScore();
        displayLevel();
        displayRowsCleared();
    }
    private void displayScore()
    {
        int score = scoreController.getScore();
        scoreText.text = score.ToString();
    }

    private void displayLevel()
    {
        int level = LevelController.Instance.getLevel();
        levelText.text = level.ToString();
    }

    private void displayRowsCleared()
    {
        int rowsCleredCount = scoreController.getTotalRowsClear();
        rowsClearedText.text = rowsCleredCount.ToString();
    }
 
}

