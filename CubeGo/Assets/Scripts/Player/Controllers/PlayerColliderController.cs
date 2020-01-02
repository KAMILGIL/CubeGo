using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColliderController : MonoBehaviour
{
    public bool isCollising;
    public GameObject selectedCube;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("PlayerCollider"))
        {
            
            isCollising = true;
            selectedCube = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("PlayerCollider"))
        {
            isCollising = false;
            selectedCube = null;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.gameObject.CompareTag("PlayerCollider"))
        {
            isCollising = true;
            selectedCube = other.gameObject;
        }
    }
}
