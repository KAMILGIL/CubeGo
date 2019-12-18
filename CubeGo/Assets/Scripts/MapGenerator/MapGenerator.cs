using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject platformPrefab;

    private Vector3 nextPlatform = Vector3.forward * 5 + Vector3.up * 4;

    private void Start()
    {
        Instantiate(platformPrefab, Vector3.zero, Quaternion.identity);
        Instantiate(platformPrefab, Vector3.forward * 5 + Vector3.up * 4, Quaternion.Euler(Vector3.right * 90f));
    }

    public void MovedRight(GameObject platform)
    {
        
    }

    public void MovedLeft(GameObject platform)
    {
        
    }

    public void MovedForward(GameObject platform)
    {
        
    }

    public void MovedBackward(GameObject platform)
    {
        
    }
}
