using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public int rowsAmount, rowsLength;

    public PlatformType platformType; 

    private GameObject[,] blocks;

    private void Start()
    {
        blocks = new GameObject[rowsAmount, rowsLength];
        GetBlocks();
    }

    private void GetBlocks()
    {
        GameObject child;
        for (int i = 0; i < transform.childCount; i++)
        {
            child = transform.GetChild(i).gameObject;
            blocks[(int)child.transform.localPosition.y, Math.Abs((int)child.transform.localPosition.x)] = child;
            child.transform.localPosition += new Vector3(rowsLength / 2, 0, -rowsAmount / 2);
        }
    }
}

public enum PlatformType
{
    Cars, 
    MovingBlocks, 
    River, 
    CarsAndRiver
}