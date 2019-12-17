using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.PlayerLoop;

/* 
public class PlainHandler : MonoBehaviour
{
    public PlayerController playerController;

    private float forwardTime, leftTime, rightTime, backwardTime;

    private string forward = "forward", backward = "backward", right = "right", left = "left";

    private Queue moves = new Queue();

    private float multiplier = 1.5f;

    private void Start()
    {
        moves.Enqueue(forward);
        moves.Enqueue(forward);
    }

    private void Update()
    {
        if (!PlayerSmartSettings.isPlainMode)
        {
            return;
        }

        var left = new Rect(0, 0, Screen.width / 2f, Screen.height);
        var right = new Rect(Screen.width / 2f, 0, Screen.width / 2f, Screen.height);
        
        if (Input.touches.Length > 0)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Began)
            {
                playerController.StartShrinkingSkin();
            }
            if (t.phase == TouchPhase.Ended)
            {
                if (left.Contains(t.position))
                {
                    moves.Enqueue(forward);
                } 
                else if (right.Contains(t.position))
                {
                    moves.Enqueue(left);
                }
            }
        }
        
        if (CheckInputIn())
        {
            playerController.StartShrinkingSkin();
        }

        if (CheckInputMoveForward())
        {
            moves.Enqueue(forward);
        } 
        else if (CheckInputMoveLeft())
        {
            moves.Enqueue(left);
        }
        else if (CheckInputMoveBackward())
        {
            moves.Enqueue(backward);
        }
        else if (CheckInputMoveRight())
        {
            moves.Enqueue(right);
        }

        if (moves.Count == 0)
        {
            return;
        }

        var move = moves.Peek();
        
        if (move == forward && !playerController.movingAnimation.isPlaying)
        {
            moves.Dequeue();
            playerController.MoveForward();
            playerController.StartExpandingSkin();
            return;
        }
    }

    private bool CheckInputIn()
    {
        if (Input.GetKeyDown(KeyCode.A) || 
            Input.GetKeyDown(KeyCode.D) ||
            Input.GetKeyDown(KeyCode.Z) ||
            Input.GetKeyDown(KeyCode.C))
        {
            return true;
        }
        return false;
    }

    private bool CheckInputMoveForward()
    {
        if (Input.GetKeyUp(KeyCode.D))
        {
            return true;
        }

        return false;
    }

    private bool CheckInputMoveLeft()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            return true;
        }

        return false;
    }

    private bool CheckInputMoveBackward()
    {
        if (Input.GetKeyUp(KeyCode.Z))
        {
            return true;
        }

        return false;
    }

    private bool CheckInputMoveRight()
    {
        if (Input.GetKeyUp(KeyCode.C))
        {
            return true;
        }

        return false;
    }
}
*/

public class PlainHandler : MonoBehaviour
{
    public PlayerController playerController;

    private float forwardTime, leftTime, rightTime, backwardTime;

    private float multiplier = 1f;

    private void Update()
    {
        if (!PlayerSmartSettings.isPlainMode)
        {
            return;
        }
        
        if (forwardTime > 0f && !playerController.movingAnimation.isPlaying)
        {
            playerController.MoveForward();
            playerController.StartExpandingSkin();
            forwardTime = 0f;
            return;
        }
        
        if (rightTime > 0f && !playerController.movingAnimation.isPlaying)
        {
            playerController.MoveRight();
            playerController.StartExpandingSkin();
            rightTime = 0f;
            return;
        }

        if (leftTime > 0f && !playerController.movingAnimation.isPlaying)
        {
            playerController.MoveLeft();
            playerController.StartExpandingSkin();
            leftTime = 0f;
            return;
        }

        if (backwardTime > 0f && !playerController.movingAnimation.isPlaying)
        {
            playerController.MoveBackward();
            playerController.StartExpandingSkin();
            backwardTime = 0f;
            return;
        }
        
        forwardTime -= Time.deltaTime;
        leftTime -= Time.deltaTime;
        rightTime -= Time.deltaTime;
        backwardTime -= Time.deltaTime;
        
        var left = new Rect(0, 0, Screen.width / 2f, Screen.height);
        var right = new Rect(Screen.width / 2f, 0, Screen.width / 2f, Screen.height);
        
        if (Input.touches.Length > 0)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Began)
            {
                playerController.StartShrinkingSkin();
            }
            if (t.phase == TouchPhase.Ended)
            {
                if (left.Contains(t.position))
                {
                    leftTime = PlayerSmartSettings.jumpingTime * multiplier;
                } 
                else if (right.Contains(t.position))
                {
                    forwardTime = PlayerSmartSettings.jumpingTime * multiplier;
                }
            }
        }
        
        if (CheckInputIn())
        {
            playerController.StartShrinkingSkin();
        }

        if (CheckInputMoveForward())
        {
            forwardTime = PlayerSmartSettings.jumpingTime * multiplier;
            return;
        }

        if (CheckInputMoveLeft())
        {
            leftTime = PlayerSmartSettings.jumpingTime * multiplier;
            return;
        }

        if (CheckInputMoveBackward())
        {
            backwardTime = PlayerSmartSettings.jumpingTime *  multiplier;
            return;
        }

        if (CheckInputMoveRight())
        {
            rightTime = PlayerSmartSettings.jumpingTime * multiplier;
            return;
        }
    }

    private bool CheckInputIn()
    {
        if (Input.GetKeyDown(KeyCode.A) || 
            Input.GetKeyDown(KeyCode.D) ||
            Input.GetKeyDown(KeyCode.Z) ||
            Input.GetKeyDown(KeyCode.C))
        {
            return true;
        }
        return false;
    }

    private bool CheckInputMoveForward()
    {
        if (Input.GetKeyUp(KeyCode.D))
        {
            return true;
        }

        return false;
    }

    private bool CheckInputMoveLeft()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            return true;
        }

        return false;
    }

    private bool CheckInputMoveBackward()
    {
        if (Input.GetKeyUp(KeyCode.Z))
        {
            return true;
        }

        return false;
    }

    private bool CheckInputMoveRight()
    {
        if (Input.GetKeyUp(KeyCode.C))
        {
            return true;
        }

        return false;
    }
}