using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CornerController : MonoBehaviour
{
    public int length, width, height; // length is z axis width is x axis height is y 

    public PlatformType horizontal, verticalType; 

    public GameObject[,] horizontalBlocks, verticalBlocks;
    
    public Vector3 deltaVector;

    public int layer; 

    private void Start()
    {
        deltaVector = new Vector3(0, height, length);
        GetBlocks();
    }

    private void GetBlocks()
    {
        horizontalBlocks = new GameObject[length, width];
        verticalBlocks = new GameObject[height, width];

        GameObject child; 
        
        for (int i = 0; i < transform.childCount; i++)
        {
            child = transform.GetChild(i).gameObject;

            if (child.transform.localPosition.y == 0)
            {
                horizontalBlocks[(int)child.transform.localPosition.z, Math.Abs((int)child.transform.localPosition.x)] = child;
            }
            else
            {
                verticalBlocks[(int) child.transform.localPosition.y - 1,
                    Math.Abs((int) child.transform.localPosition.x)] = child;
            }
        }
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}