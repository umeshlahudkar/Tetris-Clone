using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Control the Movement of the Tetrominos and after reaching down of the board calls the next tetromino.
/// </summary>
public class movement_Controller : MonoBehaviour
{
    private float fallTime = 0;
    private float fallSpeed ;     // Speed for moving Down

    [SerializeField] private bool allowRotation;
    [SerializeField] private bool limitRotation;

    private float movementSpeed = 0.05f;
    private float moventTimer = 0;
    private bool immediateMove = false;

    BoardController boardController;
    SpwanManager spwanManager;
    bool isGameOver = false;

    private void Start()
    {
        boardController = FindObjectOfType<BoardController>();
        spwanManager = FindObjectOfType<SpwanManager>();
        fallSpeed = 1.0f - (LevelController.Instance.getLevel() * 0.1f);    //as the level increse ,decrement speed
    }

    void Update()
    {
        if(!isGameOver)
        {
            userInput();
        }
    }

    private void userInput()
    {
        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            moventTimer = 0;
            immediateMove = false;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveRight();
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveLeft();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            rotate();
        }

        if (Input.GetKey(KeyCode.DownArrow) || (Time.time - fallTime) >= fallSpeed)
        {
            moveDown();
        }

        if(Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            spwanManager.swap();
        }
    }

    private void moveRight()
    {
        if (immediateMove)
        {
            if (moventTimer < movementSpeed)
            {
                moventTimer += Time.deltaTime;
                return;
            }
        }

        moventTimer = 0;
        immediateMove = true;

        this.transform.position += new Vector3(1, 0, 0);
        if (IsValidPos())
        {
        }
        else
        {
            this.transform.position += new Vector3(-1, 0, 0);
        }
    }

    private void moveLeft()
    {
        if (immediateMove)
        {
            if (moventTimer < movementSpeed)
            {
                moventTimer += Time.deltaTime;
                return;
            }
        }

        immediateMove = true;
        moventTimer = 0;

        this.transform.position += new Vector3(-1, 0, 0);
        if (IsValidPos())
        {
        }
        else
        {
            this.transform.position += new Vector3(1, 0, 0);
        }
    }

    private void rotate()
    {
        if (allowRotation)
        {
            if (limitRotation)
            {
                if (this.transform.eulerAngles.z >= 90)
                {
                    this.transform.Rotate(0, 0, -90);
                }
                else
                {
                    this.transform.Rotate(0, 0, 90);
                }
            }
            else
            {
                this.transform.Rotate(0, 0, 90);
            }

            if (IsValidPos())
            {
            }
            else
            {
                if (limitRotation)
                {
                    if (this.transform.eulerAngles.z >= 90)
                    {
                        this.transform.Rotate(0, 0, -90);
                    }
                    else
                    {
                        this.transform.Rotate(0, 0, 90);
                    }
                }
                else
                {
                    this.transform.Rotate(0, 0, -90);
                }
            }
        }
    }

    private void moveDown()
    {
        if (immediateMove)
        {
            if (moventTimer < movementSpeed)
            {
                moventTimer += Time.deltaTime;
                return;
            }
        }

        moventTimer = 0;
        immediateMove = true;

        this.transform.position += new Vector3(0, -1, 0);
        fallTime = Time.time;
        if (IsValidPos())
        {
        }
        else
        {
            this.transform.position += new Vector3(0, 1, 0);
            NextMove();
        }
    }

    // After reaching the down , Update the Board and calls the Instantiate function
    private void NextMove()
    {
        boardController.updateBoard(this.transform);
        boardController.deleteRow();

        if (boardController.isAboveGrid(this.transform))
        {
            isGameOver = true;
        }
        else
        {
            this.enabled = false;
            spwanManager.instantiateTetromino();
        }
        
    }
   
    // Checking the Tetrominos is inside the Board and If Minos present in the board 
    private bool IsValidPos()
    {
        foreach(Transform mino in transform)
        {
            if(boardController.isInsideGrid(mino.position) == false || boardController.isPresent(mino.position) == true) {
                return false;
            }
        }
        return true;
    }
}