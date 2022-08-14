using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Takes the Input from the Level select slider and Increment the Level
/// </summary>
public class LevelController : MonoBehaviour
{
    private static LevelController instance;
    public static LevelController Instance { get { return instance; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private int level;

    private int firstLevel = 0;
    private int lastLevel = 9;

    private int rowsClearedCount;

    // Increment the level after clearing  10 rows
    public void UpdateLevel(int rowsCleared)
    {
        rowsClearedCount += rowsCleared;
        if (rowsClearedCount >= 10)
        {
            level++;
            rowsClearedCount = 0;
        }
    }

    // Takes the input from the user (Slider)
    public void setLevel(int selectedLevel)
    {
        if(selectedLevel >= firstLevel && selectedLevel <= lastLevel)
        {
            level = selectedLevel;
        }
    }

    public int getLevel()
    {
        return level;
    }
}
