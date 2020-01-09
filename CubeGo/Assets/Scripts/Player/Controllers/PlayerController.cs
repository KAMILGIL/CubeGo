using System;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class PlayerController : MonoBehaviour
{
    public CameraController cameraController;
    
    public PlayerColliderController leftCollider, leftBottomCollider,
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
    public Vector3 speed;

    private bool addedClip = false; 
    
    public Animation movingAnimation;

    private void Update()
    {
        if (!movingAnimation.isPlaying)
        {
            transform.position += speed * Time.deltaTime;
        }


        if (!addedClip)
        {
            return; 
        }

        if (Math.Abs(movingAnimation["movingAnimation"].time - SmartSettings.Data.jumpingTime) <= 0.02f) // needs to be changed to detection of animation duration 
        {
            if (bottomCollider.selectedCube.GetComponent<BlockController>().speed.magnitude > 0)
            {
                bottomCollider.selectedCube.GetComponent<BlockController>().transform.parent.parent.GetComponent<TimberController>().StartMovingAnimation();
            }
        }
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
        
        skinAnimationController.LookForward();

        if (!forwardCollider.isCollising && forwardBottomCollider.isCollising)
        {
            if (!BlockIsFree(forwardBottomCollider.selectedCube))
            {
                return;
            }

            target = Vector3.up;
            targetRotation = floor;
            InitMovement(forwardBottomCollider.selectedCube);
            return; 
        }

        if (forwardCollider.isCollising)
        {
            target = Vector3.up; 
            targetRotation = floor;
            InitMovement(forwardCollider.selectedCube);
            return;
        }

        if (!forwardCollider.isCollising && !forwardBottomCollider.isCollising && bottomCollider.selectedCube.GetComponent<BlockController>().blockType == BlockType.Edge)
        {
            transform.SetParent(bottomCollider.selectedCube.transform);
            target = Vector3.forward;
            targetRotation = -wallUp;
            InitMovement(bottomCollider.selectedCube);
            return;
        }
    }

    public void MoveBackward()
    {
        if (movingAnimation.isPlaying)
        {
            return;
        }
        
        skinAnimationController.LookBack();

        if (!backCollider.isCollising && backBottomCollider.isCollising)
        {
            if (!BlockIsFree(backBottomCollider.selectedCube))
            {
                return;
            }

            if (backBottomCollider.selectedCube.GetComponent<BlockController>().blockType == BlockType.Edge)
            {
                target = Vector3.forward;
                targetRotation = -wallUp; 
            }
            else
            {
                target = Vector3.up;
                targetRotation = floor;
            }


            InitMovement(backBottomCollider.selectedCube);
            return;
        }

        if (backCollider.isCollising)
        {
            if (backCollider.selectedCube.GetComponent<BlockController>().speed.magnitude > 0)
            {
                return;
            }
            target = Vector3.up;
            targetRotation = floor;
            InitMovement(backCollider.selectedCube);
            return;
        }

        if (!backCollider.isCollising && !backBottomCollider.isCollising)
        {
            if (bottomCollider.selectedCube.GetComponent<BlockController>().blockType != BlockType.Edge)
            {
                return;
            }
            target = Vector3.up;
            targetRotation = floor;
            InitMovement(bottomCollider.selectedCube);
            return;
        }
    }

    public void MoveRight()
    {
        if (movingAnimation.isPlaying)
        {
            return;
        }
        
        skinAnimationController.LookRight();
        
        if (!rightCollider.isCollising && rightBottomCollider.isCollising)
        {
            if (!BlockIsFree(rightBottomCollider.selectedCube))
            {
                return;
            }

            
            InitMovement(rightBottomCollider.selectedCube);
            return;
        }

        if (rightCollider.isCollising)
        {
            if (rightCollider.selectedCube.GetComponent<BlockController>().speed.magnitude > 0)
            {
                return;
            }
            target = Vector3.up;
            targetRotation = floor; 
            InitMovement(rightCollider.selectedCube);
            return;
        }

        if (!rightCollider.isCollising && !rightBottomCollider.isCollising)
        {
            //target = bottomCollider.selectedCube.transform.position + Vector3.right; 
            //targetRotation = leftWall;
            //InitMovement();
            return;
        }
    }

    public void MoveLeft()
    {
        if (movingAnimation.isPlaying)
        {
            return;
        }
        
        skinAnimationController.LookLeft();
        
        if (!leftCollider.isCollising && leftBottomCollider.isCollising)
        {
            if (!BlockIsFree(leftBottomCollider.selectedCube))
            {
                return;
            }
            
            
            InitMovement(leftBottomCollider.selectedCube);
            return;
        }

        if (leftCollider.isCollising)
        {
            if (leftCollider.selectedCube.GetComponent<BlockController>().speed.magnitude > 0)
            {
                return;
            }
            target = Vector3.right;
            targetRotation = leftWall;
            InitMovement(leftCollider.selectedCube);
            return;
        }

        if (!leftCollider.isCollising && !leftBottomCollider.isCollising)
        {
            //target = bottomCollider.selectedCube.transform.position + Vector3.up;
            //targetRotation = floor; 
            //InitMovement();
            return;
        }
    }

    private bool BlockIsFree(GameObject block)
    {
        BlockController blockController = block.GetComponent<BlockController>();
        if (blockController == null)
        {
            return true;
        }

        if (blockController.blockType == BlockType.Axe ||
            blockController.blockType == BlockType.Empty ||
            blockController.blockType == BlockType.River ||
            blockController.blockType == BlockType.RoadDark ||
            blockController.blockType == BlockType.RoadLight ||
            blockController.blockType == BlockType.Edge ||
            blockController.blockType == BlockType.Roll || 
            blockController.blockType == BlockType.Saw || 
            blockController.blockType == BlockType.SawStart || 
            blockController.blockType == BlockType.SawEnd)
        {
            return true;
        }
        
        return false; 
    }

    public void StartShrinkingSkin()
    {
        skinAnimationController.StartSkinShrinkingAnimation();
    }

    public void StartExpandingSkin()
    {
        skinAnimationController.StartSkinExpandingAnimation();
    }

    private void InitMovement(GameObject targetBlock) // add argument BlockController targetBlock in order make shaking on timber nice 
    {
        transform.SetParent(targetBlock.transform);
        StartMovingAnimation();
        cameraController.MoveCamera(target);
        skinAnimationController.PlayJumpingAnimation();
    }

    private AnimationCurve GetCurve(float initValue, float targetValue)
    {
        AnimationCurve curve;
        
        Keyframe[] keys;
        keys = new Keyframe[2];
        keys[0] = new Keyframe(0.0f, initValue);
        //keys[1] = new Keyframe(SmartSettings.Data.jumpingTime / 4, (targetValue - initValue) / 3);
        keys[1] = new Keyframe(SmartSettings.Data.jumpingTime, targetValue);
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
            if (Math.Abs(transform.localEulerAngles.x - 270) <= 1f)
            {
                targetRotation.x = 360f;
            }
            clip.SetCurve("", typeof(Transform), "localEulerAngels.x", GetCurve(transform.localEulerAngles.x, targetRotation.x));
            clip.SetCurve("", typeof(Transform), "localEulerAngels.y", GetCurve(transform.localEulerAngles.y, targetRotation.y));
            clip.SetCurve("", typeof(Transform), "localEulerAngels.z", GetCurve(transform.localEulerAngles.z, targetRotation.z));
        }
        
        movingAnimation.AddClip(clip, clip.name);
        addedClip = true;

        movingAnimation.Play("movingAnimation");
    }

    private bool AbsoluteRotationComparison(Vector3 vector1, Vector3 vector2, float epsilon)
    {
        if (Math.Abs((vector1.x + 360) % 360 - (vector2.x + 360) % 360) <= epsilon && Math.Abs((vector1.y + 360) % 360 - (vector2.y + 360) % 360) <= epsilon && Math.Abs((vector1.z + 360) % 360 - (vector2.z + 360) % 360) <= epsilon)
        {
            return true;
        }

        return false;
    }
    
    private bool VectorComparison(Vector3 vector1, Vector3 vector2, float epsilon)
    {
        if (Math.Abs(vector1.x - vector2.x) <= epsilon && Math.Abs(vector1.y - vector2.y) <= epsilon && Math.Abs(vector1.z - vector2.z) <= epsilon)
        {
            return true;
        }

        return false;
    }
}