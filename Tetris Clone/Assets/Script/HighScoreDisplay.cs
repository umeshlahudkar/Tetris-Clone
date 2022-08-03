using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Display the First Highest three Score
/// </summary>
public class HighScoreDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI highScoreText1;
    [SerializeField] private TextMeshProUGUI highScoreText2;
    [SerializeField] private TextMeshProUGUI highScoreText3;


    private void Start()
    {
        displayHighScore1();
        displayHighScore2();
        displayHighScore3();
    }
    public void displayHighScore1()
    {
        highScoreText1.text = PlayerPrefs.GetInt("HighScore1").ToString();
    }

    public void displayHighScore2()
    {
        highScoreText2.text = PlayerPrefs.GetInt("HighScore2").ToString();
    }

    public void displayHighScore3()
    {
        highScoreText3.text = PlayerPrefs.GetInt("HighScore3").ToString();
    }
}
