using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlatformController : MonoBehaviour
{
    public Vector3 size;

    public PlatformType horizontalType, verticalType;

    public PlatformData platformData; 
    
    public GameObject[,] horizontalBlocks, verticalBlocks;

    public void SetPlatformData()
    {
        platformData = new PlatformData(horizontalType, verticalType, size);
    }
    
    private void Start()
    {
        GetBlocks();
    }

    private void GetBlocks()
    {
        horizontalBlocks = new GameObject[(int)size.z, (int)size.x];
        verticalBlocks = new GameObject[(int)size.y, (int)size.x];

        GameObject child;

        for (int i = 0; i < transform.childCount; i++)
        {
            child = transform.GetChild(i).gameObject;
            
            child.GetComponent<BlockController>().SetSkin("Winter");
            
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

public class PlatformData
{
    public PlatformType horizontalType = PlatformType.Common, verticalType = PlatformType.Common;

    public Vector3 size;

    public bool isAny = false; 

    public PlatformData(PlatformType horizontalType, PlatformType verticalType, Vector3 size)
    {
        this.horizontalType = horizontalType;
        this.verticalType = verticalType;
        this.size = size;
    }

    public PlatformData(bool isAny)
    {
        this.isAny = true;
    }

    public override string ToString()
    {
        return size.x.ToString() + "-" + size.y.ToString() + "-" + size.z.ToString() + "-" + PlatformTypeExtension.ToFriendlyString(horizontalType) +
               PlatformTypeExtension.ToFriendlyString(verticalType);
    }
}

public enum PlatformType
{
    Common, 
    Cars, 
    MovingBlocks, 
    River, 
    CarsAndRiver
}

public static class PlatformTypeExtension
{
    public static string ToFriendlyString(this PlatformType me)
    {
        switch(me)
        {
            case PlatformType.Common:
                return "Common";
            case PlatformType.Cars:
                return "Cars";
            case PlatformType.MovingBlocks:
                return "MovingBlocks";
            case PlatformType.River:
                return "River";
            case PlatformType.CarsAndRiver:
                return "CarsAndRiver";
            default:
                return "Get your damn dirty hands off me you FILTHY APE!";
        }
    }
}