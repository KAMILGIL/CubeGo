using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ColliderCreator : MonoBehaviour
{
    public GameObject colliderPrefab;
    
    private GameObject leftCollider, leftBottomCollider,
        forwardCollider, forwardBottomCollider,
        rightCollider, rightBottomCollider,
        backCollider, backBottomCollider,
        bottomCollider;

    private PlayerColliderController forwardData, forwardBottomData,
        rightData, rightBottomData,
        leftData, leftBottomData,
        backData, backBottomData,
        bottomData;

    private void Start()
    {
        SetColliders();
    }

    public void SetPlayerController(PlayerController playerController) 
    {
        playerController.forwardCollider = forwardData;
        playerController.forwardBottomCollider = forwardBottomData;

        playerController.leftCollider = leftData;
        playerController.leftBottomCollider = leftBottomData;

        playerController.rightCollider = rightData;
        playerController.rightBottomCollider = rightBottomData;

        playerController.backCollider = backData;
        playerController.backBottomCollider = backBottomData;

        playerController.bottomCollider = bottomData; 
    }

    private void SetColliders()
    {
        // setting colliders
        leftCollider = Instantiate(colliderPrefab, Vector3.left, Quaternion.identity);
        leftCollider.name = "LeftCollider";
        leftCollider.transform.SetParent(transform, false);
        leftData = leftCollider.GetComponent<PlayerColliderController>();

        leftBottomCollider = Instantiate(colliderPrefab, Vector3.left + Vector3.down, Quaternion.identity);
        leftBottomCollider.name = "LeftBottomCollider";
        leftBottomCollider.transform.SetParent(transform, false);
        leftBottomData = leftBottomCollider.GetComponent<PlayerColliderController>();

        forwardCollider = Instantiate(colliderPrefab, Vector3.forward, Quaternion.identity);
        forwardCollider.name = "ForwardCollider";
        forwardCollider.transform.SetParent(transform, false);
        forwardData = forwardCollider.GetComponent<PlayerColliderController>();

        forwardBottomCollider = Instantiate(colliderPrefab, Vector3.forward + Vector3.down, Quaternion.identity);
        forwardBottomCollider.name = "ForwardBottomCollider";
        forwardBottomCollider.transform.SetParent(transform, false);
        forwardBottomData = forwardBottomCollider.GetComponent<PlayerColliderController>();

        rightCollider = Instantiate(colliderPrefab, Vector3.right, Quaternion.identity);
        rightCollider.name = "RightCollider";
        rightCollider.transform.SetParent(transform, false);
        rightData = rightCollider.GetComponent<PlayerColliderController>();

        rightBottomCollider = Instantiate(colliderPrefab, Vector3.right + Vector3.down, Quaternion.identity);
        rightBottomCollider.name = "RightBottomCollider";
        rightBottomCollider.transform.SetParent(transform, false);
        rightBottomData = rightBottomCollider.GetComponent<PlayerColliderController>();

        backCollider = Instantiate(colliderPrefab, Vector3.back, Quaternion.identity);
        backCollider.name = "BackCollider";
        backCollider.transform.SetParent(transform, false);
        backData = backCollider.GetComponent<PlayerColliderController>();

        backBottomCollider = Instantiate(colliderPrefab, Vector3.back + Vector3.down, Quaternion.identity);
        backBottomCollider.name = "BackBottomCollider";
        backBottomCollider.transform.SetParent(transform, false);
        backBottomData = backBottomCollider.GetComponent<PlayerColliderController>();

        bottomCollider = Instantiate(colliderPrefab, Vector3.down, Quaternion.identity);
        bottomCollider.name = "BottomCollider";
        bottomCollider.transform.SetParent(transform, false);
        bottomData = bottomCollider.GetComponent<PlayerColliderController>();
    }
}