using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComplicatedHandler : MonoBehaviour
{

    public PlayerController playerController;

    private float forwardTime, leftTime, rightTime, backwardTime;

    private float multiplier = 1f;

    private Vector2 touchStartPosition; 

    private void Update()
    {
        if (PlayerSmartSettings.isPlainMode)
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
                print("ended");
                print(deltaVector);

                if (Math.Abs(deltaVector.x) >= 0.8f && Math.Abs(deltaVector.y) >= 0.8f)
                {
                    if (deltaVector.x < 0 && deltaVector.y > 0)
                    {
                        rightTime = PlayerSmartSettings.jumpingTime * multiplier;
                    }
                    else if (deltaVector.x < 0 && deltaVector.y < 0)
                    {
                        forwardTime = PlayerSmartSettings.jumpingTime * multiplier;
                    }
                    else if (deltaVector.x > 0 && deltaVector.y > 0)
                    {
                        backwardTime = PlayerSmartSettings.jumpingTime * multiplier;
                    }
                    else if (deltaVector.x > 0 && deltaVector.y < 0)
                    {
                        leftTime = PlayerSmartSettings.jumpingTime * multiplier;
                    }
                }
                else 
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
            Input.GetKeyDown(KeyCode.W) ||
            Input.GetKeyDown(KeyCode.S))
        {
            return true;
        }
        return false;
    }

    private bool CheckInputMoveForward()
    {
        if (Input.GetKeyUp(KeyCode.W))
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
        if (Input.GetKeyUp(KeyCode.S))
        {
            return true;
        }

        return false;
    }

    private bool CheckInputMoveRight()
    {
        if (Input.GetKeyUp(KeyCode.D))
        {
            return true;
        }

        return false;
    }
}
