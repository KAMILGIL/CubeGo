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

    public GameObject platform;

    public ColliderController leftCollider, leftBottomCollider,
        forwardCollider, forwardBottomCollider,
        rightCollider, rightBottomCollider,
        backCollider, backBottomCollider,
        bottomCollider;

    public MapGenerator mapGenerator;
    
    private GameObject skinCenter;

    private SkinAnimationController skinAnimationController;

    private ColliderCreator colliderCreator;
    private PlainHandler plainHanlder;
    private ComplicatedHandler complicatedHandler;
    
    private Vector3 target, targetRotation, wallUp = new Vector3(-90f, 0, 0), leftWall = new Vector3(0f, 0f, -90f), floor = Vector3.zero;

    public Animation movingAnimation;

    private void Update()
    {
        
    }

    private void Start()
    {
        skinCenter = transform.GetChild(0).gameObject;

        skinAnimationController = skinCenter.GetComponent<SkinAnimationController>();

        colliderCreator = GetComponent<ColliderCreator>();
        plainHanlder = GetComponent<PlainHandler>();
        complicatedHandler = GetComponent<ComplicatedHandler>();

        plainHanlder.playerController = this;
        complicatedHandler.playerController = this;
        
        colliderCreator.SetPlayerController(this);

        movingAnimation = gameObject.AddComponent<Animation>();
    }

    public void MoveForward()
    {
        if (movingAnimation.isPlaying)
        {
            return;
        }
        
        mapGenerator.MovedForward(bottomCollider.GetPlatform());
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
            targetRotation = wallUp;
            InitMovement();
            return;
        }

        if (!forwardCollider.isCollising && !forwardBottomCollider.isCollising)
        {
            target = bottomCollider.selectedCube.transform.position + Vector3.up;
            targetRotation = floor;
            InitMovement();
            return;
        }
    }

    public void MoveBackward()
    {
        if (movingAnimation.isPlaying)
        {
            return;
        }
        
        //mapGenerator.MovedBackward(bottomCollider.GetPlatform());
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
            targetRotation = floor;
            InitMovement();
            return;
        }

        if (!backCollider.isCollising && !backBottomCollider.isCollising)
        {
            target = bottomCollider.selectedCube.transform.position + Vector3.back;
            targetRotation = wallUp;
            InitMovement();
            return;
        }
    }

    public void MoveRight()
    {
        if (movingAnimation.isPlaying)
        {
            return;
        }
        
        mapGenerator.MovedRight(bottomCollider.GetPlatform());
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
            targetRotation = floor; 
            InitMovement();
            return;
        }

        if (!rightCollider.isCollising && !rightBottomCollider.isCollising)
        {
            target = bottomCollider.selectedCube.transform.position + Vector3.right; 
            targetRotation = leftWall;
            InitMovement();
            return;
        }
    }

    public void MoveLeft()
    {
        if (movingAnimation.isPlaying)
        {
            return;
        }
        
        mapGenerator.MovedLeft(bottomCollider.GetPlatform());
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
            targetRotation = leftWall;
            InitMovement();
            return;
        }

        if (!leftCollider.isCollising && !leftBottomCollider.isCollising)
        {
            target = bottomCollider.selectedCube.transform.position + Vector3.up;
            targetRotation = floor; 
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
        StartMovingAnimation();
        // say mapGenerator that player moved 
        cameraController.MoveCamera(target);
        skinAnimationController.PlayJumpingAnimation();
    }

    private AnimationCurve GetCurve(float initValue, float targetValue)
    {
        AnimationCurve curve;
        
        Keyframe[] keys;
        keys = new Keyframe[2];
        keys[0] = new Keyframe(0.0f, initValue);
        keys[1] = new Keyframe(PlayerSmartSettings.jumpingTime, targetValue);
        curve = new AnimationCurve(keys);

        return curve;
    }

    private void StartMovingAnimation()
    {
        AnimationClip clip = new AnimationClip();
        clip.name = "movingAnimation";
        clip.legacy = true;

        clip.SetCurve("", typeof(Transform), "localPosition.x", GetCurve(transform.localPosition.x, target.x));
        clip.SetCurve("", typeof(Transform), "localPosition.y", GetCurve(transform.localPosition.y, target.y));
        clip.SetCurve("", typeof(Transform), "localPosition.z", GetCurve(transform.localPosition.z, target.z));

        if (!AbsoluteRotationComparison(transform.localEulerAngles, targetRotation, 1f))
        {
            clip.SetCurve("", typeof(Transform), "localEulerAngels.x", GetCurve(transform.localEulerAngles.x, targetRotation.x));
            clip.SetCurve("", typeof(Transform), "localEulerAngels.y", GetCurve(transform.localEulerAngles.x, targetRotation.y));
            clip.SetCurve("", typeof(Transform), "localEulerAngels.z", GetCurve(transform.localEulerAngles.x, targetRotation.z));
        } 
        
        movingAnimation.AddClip(clip, clip.name);

        movingAnimation.Play("movingAnimation");
    }

    private bool CompareVectors(Vector3 vector1, Vector3 vector2, float epsilon)
    {
        if (Math.Abs(vector1.x - vector2.x) <= epsilon && Math.Abs(vector1.y - vector2.y) <= epsilon && Math.Abs(vector1.z - vector2.z) <= epsilon)
        {
            return true;
        }

        return false;
    }
    
    private bool AbsoluteRotationComparison(Vector3 vector1, Vector3 vector2, float epsilon)
    {
        if (Math.Abs((vector1.x + 360) % 360 - (vector2.x + 360) % 360) <= epsilon && Math.Abs((vector1.y + 360) % 360 - (vector2.y + 360) % 360) <= epsilon && Math.Abs((vector1.z + 360) % 360 - (vector2.z + 360) % 360) <= epsilon)
        {
            return true;
        }

        return false;
    }
}