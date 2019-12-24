using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    public GameObject platform;

    private void Start()
    {
        platform = transform.parent.gameObject;
    }
} 
