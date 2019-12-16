using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class PlayerController : MonoBehaviour
{
    public CameraController cameraController;

    public ColliderController leftCollider, leftBottomCollider,
        forwardCollider, forwardBottomCollider,
        rightCollider, rightBottomCollider,
        backCollider, backBottomCollider,
        bottomCollider;
    
    private GameObject skinCenter;

    private SkinAnimationController skinAnimationController;

    private ColliderCreator colliderCreator;
    private PlainHandler plainHanlder;
    private ComplicatedHandler complicatedHandler;
    
    private Vector3 target, speed, wallUp = new Vector3(270f, 0, 0), leftWall = new Vector3(0f, 0f, 270f), floor = Vector3.zero;

    private bool isMoving;
    
    private void Start()
    {
        ManageChildren();

        colliderCreator = GetComponent<ColliderCreator>();
        plainHanlder = GetComponent<PlainHandler>();
        complicatedHandler = GetComponent<ComplicatedHandler>();

        plainHanlder.playerController = this;
        complicatedHandler.playerController = this;
        
        colliderCreator.SetPlayerController(this);
    }

    private void FixedUpdate()
    {
        transform.position += speed;
        if (transform.position == target)
        {
            speed = Vector3.zero;
            isMoving = false;
        }
    }

    public void MoveForward()
    {
        if (isMoving)
        {
            return;
        }
        skinAnimationController.LookForward();
        
        if (!forwardCollider.isCollising && forwardBottomCollider.isCollising)
        {
            target = forwardBottomCollider.selectedCube.transform.position + transform.position -
                     bottomCollider.selectedCube.transform.position;
            // rotation doesn't change
            InitMovement();
            return; 
        }

        if (forwardCollider.isCollising)
        {
            target = forwardCollider.selectedCube.transform.position + Vector3.back; 
            transform.rotation = Quaternion.Euler(wallUp);
            skinAnimationController.RotateSkinFromFloorToWallUp();
            InitMovement();
            return;
        }

        if (!forwardCollider.isCollising && !forwardBottomCollider.isCollising)
        {
            target = bottomCollider.selectedCube.transform.position + Vector3.up;
            transform.rotation = Quaternion.Euler(floor);
            InitMovement();
            return;
        }
    }

    public void MoveBackward()
    {
        if (isMoving)
        {
            return;
        }
        skinAnimationController.LookBack();
        
        if (!backCollider.isCollising && backBottomCollider.isCollising)
        {
            target = backBottomCollider.selectedCube.transform.position + transform.position -
                     bottomCollider.selectedCube.transform.position;
            InitMovement();
            return;
        }

        if (backCollider.isCollising)
        {
            target = backCollider.selectedCube.transform.position + Vector3.up; 
            transform.rotation = Quaternion.Euler(floor);
            InitMovement();
            return;
        }

        if (!backCollider.isCollising && !backBottomCollider.isCollising)
        {
            target = bottomCollider.selectedCube.transform.position + Vector3.back;
            transform.rotation = Quaternion.Euler(wallUp); 
            InitMovement();
            return;
        }
    }

    public void MoveRight()
    {
        if (isMoving)
        {
            return;
        }
        
        skinAnimationController.LookRight();
        if (!rightCollider.isCollising && rightBottomCollider.isCollising)
        {
            target = rightBottomCollider.selectedCube.transform.position + transform.position -
                     bottomCollider.selectedCube.transform.position;
            InitMovement();
            return;
        }

        if (rightCollider.isCollising)
        {
            target = rightCollider.selectedCube.transform.position + Vector3.up;
            transform.rotation = Quaternion.Euler(floor); 
            InitMovement();
            return;
        }

        if (!rightCollider.isCollising && !rightBottomCollider.isCollising)
        {
            target = bottomCollider.selectedCube.transform.position + Vector3.right; 
            transform.rotation = Quaternion.Euler(leftWall);
            InitMovement();
            return;
        }
    }

    public void MoveLeft()
    {
        if (isMoving)
        {
            return;
        }
        
        skinAnimationController.LookLeft();
        if (!leftCollider.isCollising && leftBottomCollider.isCollising)
        {
            target = leftBottomCollider.selectedCube.transform.position + transform.position -
                     bottomCollider.selectedCube.transform.position;
            InitMovement();
            return;
        }

        if (leftCollider.isCollising)
        {
            target = leftCollider.selectedCube.transform.position + Vector3.right;
            transform.rotation = Quaternion.Euler(leftWall);
            InitMovement();
            return;
        }

        if (!leftCollider.isCollising && !leftBottomCollider.isCollising)
        {
            target = bottomCollider.selectedCube.transform.position + Vector3.up;
            transform.rotation = Quaternion.Euler(floor); 
            InitMovement();
            return;
        }
    }

    public void StartShrinkingSkin()
    {
        skinAnimationController.StartSkinShrinkingAnimation();
    }

    public void StartExpandingSkin()
    {
        skinAnimationController.StartSkinExpandingAnimation();
    }

    private void InitMovement()
    {
        speed = -(transform.position - target) / 9;
        cameraController.MoveCamera(target);
        StartExpandingSkin();
        skinAnimationController.PlayJumpingAnimation();
        isMoving = true;
    }

    private void ManageChildren()
    {
        skinCenter = transform.GetChild(0).gameObject;

        skinAnimationController = skinCenter.AddComponent<SkinAnimationController>();
    }
}