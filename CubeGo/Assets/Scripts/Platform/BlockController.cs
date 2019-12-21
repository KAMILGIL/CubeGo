using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    public GameObject platform;
    private PlatformController platformController; 

    private void Start()
    {
        platform = transform.parent.transform.parent.transform.parent.gameObject;
        platformController = platform.GetComponent<PlatformController>();
    }
} 
