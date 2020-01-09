using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.PlayerLoop;
using SmartSettings;

public class PlainHandler : MonoBehaviour
{
    public PlayerController playerController;

    private float forwardTime, leftTime, rightTime, backwardTime;

    private float multiplier = 0.7f;

    private Vector2 touchStartPosition; 

    private void Update()
    {
        if (!SmartSettings.Data.isPlainMode)
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
                touchStartPosition = t.position;
            }
            if (t.phase == TouchPhase.Ended)
            {
                var deltaVector = touchStartPosition - t.position;
                print(deltaVector);

                if (Math.Abs(deltaVector.x) >= 1.4f && Math.Abs(deltaVector.y) >= 1.4f)
                {
                    if (deltaVector.x < 0 && deltaVector.y > 0)
                    {
                        rightTime = SmartSettings.Data.jumpingTime * multiplier;
                    }
                    else if (deltaVector.x < 0 && deltaVector.y < 0)
                    {
                        forwardTime = SmartSettings.Data.jumpingTime * multiplier;
                    }
                    else if (deltaVector.x > 0 && deltaVector.y > 0)
                    {
                        backwardTime = SmartSettings.Data.jumpingTime * multiplier;
                    }
                    else if (deltaVector.x > 0 && deltaVector.y < 0)
                    {
                        leftTime = SmartSettings.Data.jumpingTime * multiplier;
                    }
                }
                else if (left.Contains(t.position))
                {
                    leftTime = SmartSettings.Data.jumpingTime * multiplier;
                } 
                else if (right.Contains(t.position))
                {
                    forwardTime = SmartSettings.Data.jumpingTime * multiplier;
                }
            }
        }
        
        if (CheckInputIn())
        {
            playerController.StartShrinkingSkin();
        }

        if (CheckInputMoveForward())
        {
            forwardTime = SmartSettings.Data.jumpingTime * multiplier;
            return;
        }

        if (CheckInputMoveLeft())
        {
            leftTime = SmartSettings.Data.jumpingTime * multiplier;
            return;
        }

        if (CheckInputMoveBackward())
        {
            backwardTime = SmartSettings.Data.jumpingTime *  multiplier;
            return;
        }

        if (CheckInputMoveRight())
        {
            rightTime = SmartSettings.Data.jumpingTime * multiplier;
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