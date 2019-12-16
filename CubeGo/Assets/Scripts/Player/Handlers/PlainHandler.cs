using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlainHandler : MonoBehaviour
{
    public PlayerController playerController;

    private float forwardTime, leftTime, rightTime, backwardTime;

    private void Update()
    {
        if (!PlayerSmartSettings.isPlainMode)
        {
            return;
        }
        
        if (forwardTime > 0f)
        {
            playerController.MoveForward();
            playerController.StartExpandingSkin();
            forwardTime = 0f;
            return;
        }
        
        if (rightTime > 0f)
        {
            playerController.MoveRight();
            playerController.StartExpandingSkin();
            rightTime = 0f;
            return;
        }

        if (leftTime > 0f)
        {
            playerController.MoveLeft();
            playerController.StartExpandingSkin();
            leftTime = 0f;
            return;
        }

        if (backwardTime > 0f)
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
                    leftTime = PlayerSmartSettings.jumpingTime;
                } 
                else if (right.Contains(t.position))
                {
                    forwardTime = PlayerSmartSettings.jumpingTime;
                }
            }
        }
        
        if (CheckInputIn())
        {
            playerController.StartShrinkingSkin();
        }

        if (CheckInputMoveForward())
        {
            forwardTime = PlayerSmartSettings.jumpingTime;
            return;
        }

        if (CheckInputMoveLeft())
        {
            leftTime = PlayerSmartSettings.jumpingTime;
            return;
        }

        if (CheckInputMoveBackward())
        {
            backwardTime = PlayerSmartSettings.jumpingTime;
            return;
        }

        if (CheckInputMoveRight())
        {
            rightTime = PlayerSmartSettings.jumpingTime;
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
