using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderController : MonoBehaviour
{
    public bool isCollising;
    public GameObject selectedCube, selectedPlatform;

    private void OnTriggerEnter(Collider other)
    {
        isCollising = true;
        selectedCube = other.gameObject;
        if (other.gameObject.CompareTag("Block"))
        {
            selectedCube.GetComponent<BlockController>().InitPlatform();
            selectedPlatform = selectedCube.GetComponent<BlockController>().platform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isCollising = false;
        selectedCube = null;
        if (other.gameObject.CompareTag("Block"))
        {
            selectedPlatform = null;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        isCollising = true;
        selectedCube = other.gameObject;
        if (other.gameObject.CompareTag("Block"))
        {
            selectedCube.GetComponent<BlockController>().InitPlatform();
            selectedPlatform = selectedCube.GetComponent<BlockController>().platform;
        }
    }
}
