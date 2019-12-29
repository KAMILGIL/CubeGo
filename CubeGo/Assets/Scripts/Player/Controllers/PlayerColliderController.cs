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
        isCollising = true;
        selectedCube = other.gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        isCollising = false;
        selectedCube = null;
    }

    private void OnTriggerStay(Collider other)
    {
        isCollising = true;
        selectedCube = other.gameObject;
    }

    public GameObject GetPlatform()
    {
        return selectedCube.GetComponent<BlockColliderController>().platform;
    }
}
