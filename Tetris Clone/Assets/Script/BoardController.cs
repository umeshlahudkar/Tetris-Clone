using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Update the Position of the Minos in the board ande check if rows is full or not.
/// if it is full, deleting it and moves all the rows above it to the down.
/// </summary>
public class BoardController : MonoBehaviour
{
    private static int width = 10;           // Board Width size
    private static int height = 20;          // Board Height size
    private int maxHeight = 18;              // Max height upto which Tetrominos can be arranged in Board
    private float offset = 0.5f;
    private Transform[,] board;              // To store the position of minos in Board
    private int numberOfRowCleared = 0;

    [SerializeField] private GameObject prefabGridTile;
    [SerializeField] public DisplayController displayController;
    [SerializeField] private ScoreController scoreController;
    [SerializeField] private GameObject gameOverScreen;

    void Start()
    {
        drawGrid();
        board = new Transform[width, height];
    }

    // Check y rows full or not 
    public bool isRowFull(int y)
    {
        for (int x = 0; x < width; x++)
        {
            if (board[x, y] == false)
            {
                return false;
            }
        }
        return true;
    }

    // deleting y rows, each index contain 1 minos so destroying it make its position null
    public void deleteRow(int y)
    {
        for (int x = 0; x < width; x++)
        {
            Destroy(board[x, y].gameObject);
            board[x, y] = null;
        }
    }

    // Moving y rows one position down
    public void moveRowDown(int y)
    {
        for (int x = 0; x < width; x++)
        {
            if (board[x, y] != null)
            {
                board[x, y - 1] = board[x, y];
                board[x, y] = null;
                board[x, y - 1].position += new Vector3(0, -1, 0);
            }
        }
    }

    public void moveAllRowsDown(int y)
    {
        for (int i = y; i < height; i++)
        {
            moveRowDown(i);
        }
    }

    // Deleting Rows
    public void deleteRow()
    {
        for (int y = 0; y < height; y++)
        {
            if (isRowFull(y))             // checking if rows Full or not
            {
                deleteRow(y);             // if yes then deleting row
                moveAllRowsDown(y + 1);   // above rows moving down to deleted rows position 
                y--;                      // after moving decrement y, so can again check its condition
                numberOfRowCleared++;     // keeping count of cleared rows at a time for scoring
                SoundManager.Instance.playSfxSound(SoundManager.soundName.rowCleared);
            }
        }
        scoreController.UpdateScore(numberOfRowCleared);
        LevelController.Instance.UpdateLevel(numberOfRowCleared);
        displayController.UpdateDisplay();
        numberOfRowCleared = 0;
    }

    // Updating matrix with the TetroMinos position
    public void updateBoard(Transform obj)   
    {
        foreach (Transform mino in obj)          // Iterating through each minos position
        {
            board[(int)mino.position.x, (int)mino.position.y] = mino;  // at specific index storing the minos position
        }
    }

    // Checking in the board at specific index if there is any minos present or not
    public bool isPresent(Vector2 pos)
    {
        if(!isInsideGrid(pos))
        {
            return false;
        }
        return (board[(int)pos.x, (int)pos.y]);
    }

    // Drwaing the Board
    private void drawGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameObject tile = Instantiate(prefabGridTile, this.transform);
                tile.transform.position = new Vector2(x + offset, y + offset);
            }
        }
    }

    // Checking the pos of Minos is inside the Grid
    public bool isInsideGrid(Vector2 pos)
    {
        return pos.x >= 0 && pos.x < width && pos.y >= 0;
    }

    // Checking if any minos position above the Board
    public bool isAboveGrid(Transform tetromino)
    {
        foreach (Transform mino in tetromino)     // Checking each minos in the TetroMinos
        {
            if (mino.position.y > maxHeight)      
            {
                gameOverScreen.SetActive(true);   
                SoundManager.Instance.playBgSound(SoundManager.soundName.gameOver);
                return true;
            }
        }
        return false;
    }
    
}

