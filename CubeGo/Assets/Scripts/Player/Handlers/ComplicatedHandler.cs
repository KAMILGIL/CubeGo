using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ComplicatedHandler : MonoBehaviour
{
    public PlayerController playerController;

    private float multiplier = 1f;

    private Vector2 touchStartPosition;

    public List<SmartMove> moves = new List<SmartMove>();

    private void Update()
    {
        var count = moves.Count; 
        for (int i = 0; i < count; i++)
        {
            moves[i].time -= Time.deltaTime; 
            if (moves[i].time < 0)
            {
                moves.RemoveAt(0); 
            }
            else
            {
                break; 
            }
        }

        if (SmartSettings.Data.isPlainMode)
        {
            return;
        }

        if (CheckInputIn())
        {
            playerController.StartShrinkingSkin();
        }

        if (CheckInputMoveForward())
        {
            moves.Add(new SmartMove(MoveType.Forward));
        }

        if (CheckInputMoveLeft())
        {
            moves.Add(new SmartMove(MoveType.Left));
        }

        if (CheckInputMoveBackward())
        {
            moves.Add(new SmartMove(MoveType.Backward));
        }

        if (CheckInputMoveRight())
        {
            moves.Add(new SmartMove(MoveType.Right));
        }

        if (moves.Count == 0)
        {
            return; 
        }
        if (playerController.movingAnimation.isPlaying)
        {
            return;
        }

        playerController.StartExpandingSkin();
        if (moves[0].moveType == MoveType.Forward)
        {
            playerController.MoveForward(); 
        }
        else if (moves[0].moveType == MoveType.Backward)
        {
            playerController.MoveBackward();
        }
        else if (moves[0].moveType == MoveType.Right)
        {
            playerController.MoveRight();
        }
        else if (moves[0].moveType == MoveType.Left)
        {
            playerController.MoveLeft();
        }

        moves.RemoveAt(0);
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

public enum MoveType
{
    Right, 
    Left, 
    Forward,
    Backward
}

public class SmartMove
{
    public MoveType moveType;
    public float time = 0; 

    public SmartMove(MoveType moveType)
    {
        this.time = SmartSettings.Data.jumpingTime;
        this.moveType = moveType;
    }
}