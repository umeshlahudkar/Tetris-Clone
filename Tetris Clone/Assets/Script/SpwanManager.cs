using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Instatiate the new TetroMinos and swap the Tetrominos.
/// </summary>
public class SpwanManager : MonoBehaviour
{
    [SerializeField] private GameObject[] tetrominos;  // Store all the tetrominos
    private int minNumber = 0;                         // Min Tetrominos
    private int maxNumber = 7;                         // Max TetroMinos

    [SerializeField] private Transform nextMinosPos;   // Position of the Next Tetrominos(Previw Tetrominos)
    [SerializeField] private Transform savedMinosPos;  // Position of the Saved Tetrominos (Swap Tetrominos)

    [SerializeField] private BoardController boardController;

    GameObject currentTetromino;                      // active Tetromino
    GameObject NextTetromino;                         // Preview tetromino
    GameObject savedTetromino;                        // Swap with saved tetrominos

    bool gameStarted = false;

    void Start()
    {
        instantiateTetromino();
    }

    public void instantiateTetromino()
    {
        if(!gameStarted)
        {
            //First time, When the game start Instantiate two TetroMinos 1) Current and 2) Previw tetrominos(Next Tetrominos)
            gameStarted = true;
            currentTetromino = Instantiate(tetrominos[randomNumberGenerator()]);
            currentTetromino.transform.position = this.transform.position;

            NextTetromino = Instantiate(tetrominos[randomNumberGenerator()]);
            NextTetromino.transform.position = nextMinosPos.transform.position;
            NextTetromino.GetComponent<movement_Controller>().enabled = false;

        } else
        {
            NextTetromino.transform.position = this.transform.position;
            NextTetromino.GetComponent<movement_Controller>().enabled = true;
            currentTetromino = NextTetromino;

            NextTetromino = Instantiate(tetrominos[randomNumberGenerator()]);
            NextTetromino.transform.position = nextMinosPos.transform.position;
            NextTetromino.GetComponent<movement_Controller>().enabled = false;

        }
       
    }

    // Swaping the Tetrominos with Saved Tetromino and current Tetromino
    public void swap()
    {
        if(savedTetromino == null)
        {
            // First time saves the current tetromino in saved tetromino
            savedTetromino = currentTetromino;
            savedTetromino.transform.position = savedMinosPos.transform.position;
            savedTetromino.GetComponent<movement_Controller>().enabled = false;

            instantiateTetromino();
        } else
        {
            if(canSwap(savedTetromino.transform, currentTetromino.transform))
            {
                GameObject tempObject = currentTetromino;

                currentTetromino = savedTetromino;
                currentTetromino.transform.position = tempObject.transform.position;
                currentTetromino.GetComponent<movement_Controller>().enabled = true;

                savedTetromino = tempObject;
                savedTetromino.transform.position = savedMinosPos.position;
                savedTetromino.GetComponent<movement_Controller>().enabled = false;

            }
        }
    }

    // Generates the random number between 0-7 to select the random Tetromino
    private int randomNumberGenerator()
    {
        return Random.Range(minNumber, maxNumber);
    }

    // Checking Saved and Current tetromino can swap
    public bool canSwap(Transform savedTetromino, Transform currentTetromino)
    {
        Vector2 currentPos = currentTetromino.position;
        Vector2 savedPos = savedTetromino.position;

        currentTetromino = savedTetromino;
        currentTetromino.transform.position = currentPos;
        foreach (Transform mino in currentTetromino.transform)
        {
            if (boardController.isInsideGrid(mino.position) == false || boardController.isPresent(mino.position))
            {
                savedTetromino = currentTetromino;
                savedTetromino.transform.position = savedPos;
                return false;
            }
        }
        savedTetromino = currentTetromino;
        return true;
    }
}
