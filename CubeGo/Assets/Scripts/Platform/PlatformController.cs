using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public int width, length;

    public GameObject[] blocks; 

    public void SetPlatformCenter()
    {
        GetBlocks();
        print("started");
        print(blocks.Length);
    }

    private void GetBlocks()
    {
        blocks = GameObject.FindGameObjectsWithTag("Block");
    }
}
