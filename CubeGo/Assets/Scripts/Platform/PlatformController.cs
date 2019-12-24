using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlatformController : MonoBehaviour
{
    public Vector3 size;

    public string platformData; // data format is "size.z-size.x-size.y-horizontalType-verticalType" example: "8-10-7-Common-Common"
    
    public GameObject[,] horizontalBlocks, verticalBlocks;
    
    private void Start()
    {
        GetBlocks();
    }

    public string GetData()
    {
        return size.z.ToString() + "-" + size.x.ToString() + "-" + size.y.ToString() + "-" + platformData;
    }

    private void GetBlocks()
    {
        horizontalBlocks = new GameObject[(int)size.z, (int)size.x];
        verticalBlocks = new GameObject[(int)size.y, (int)size.x];

        GameObject child; 
        
        for (int i = 0; i < transform.childCount; i++)
        {
            child = transform.GetChild(i).gameObject;

            if (child.transform.localPosition.y == 0)
            {
                horizontalBlocks[(int)child.transform.localPosition.z, Math.Abs((int)child.transform.localPosition.x)] = child;
                //Destroy(child.transform.GetChild(0).gameObject);
            }
            else
            {
                verticalBlocks[(int) child.transform.localPosition.y - 1,
                    Math.Abs((int) child.transform.localPosition.x)] = child;
                //Destroy(child.transform.GetChild(0).gameObject);
            }
        }
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}

public class PlatformSize
{
    public float length, width, height;

    public PlatformSize(float length, float width, float height)
    {
        this.length = length;
        this.width = width;
        this.height = height;
    }
}

public class PlatformData
{
    public PlatformType horizontalType = PlatformType.Common, verticalType = PlatformType.Common;

    public PlatformSize platformSize;

    public PlatformData(PlatformType horizontalType, PlatformType verticalType, PlatformSize platformSize)
    {
        this.horizontalType = horizontalType;
        this.verticalType = verticalType;
        this.platformSize = platformSize;
    }
}

public enum PlatformType
{
    Common, 
    Cars, 
    MovingBlocks, 
    River, 
    CarsAndRiver, 
    Any
}