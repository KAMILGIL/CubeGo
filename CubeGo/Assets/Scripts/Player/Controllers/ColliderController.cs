using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderController : MonoBehaviour
{
    public bool isCollising;
    public GameObject selectedCube;
 
    private void OnTriggerEnter(Collider other)
    {
        print("entered");
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
        print("staying");
        isCollising = true;
        selectedCube = other.gameObject;
    }
}
