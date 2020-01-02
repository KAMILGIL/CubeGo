﻿using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Win32.SafeHandles;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class PlatformController : MonoBehaviour
{
    public Vector3 size;

    public GameObject[,] horizontalBlocks, verticalBlocks;
    
    public LayerController layer;

    public List<Vector3> riverCoordinates = new List<Vector3>(), 
        roadCoordinates = new List<Vector3>();

    public Dictionary<Vector3, Vector3> rollEnds = new Dictionary<Vector3, Vector3>();

    private void Start()
    {
        SetSkin();
    }

    public string GetPlatformCode()
    {
        GetBlocks();
        string code = "";
        
        for (int i = 0; i < horizontalBlocks.GetLength(0) - 1; i++)
        {
            switch (horizontalBlocks[i, 0].GetComponent<BlockController>().blockType)
            {
                case BlockType.Empty:
                    code += "E";
                    break; 
                case BlockType.Plain1:
                    code += "E";
                    break; 
                case BlockType.Plain2:
                    code += "E";
                    break; 
                case BlockType.Plain3:
                    code += "E";
                    break; 
                case BlockType.Plain4:
                    code += "E";
                    break; 
                case BlockType.Plain5: 
                    code += "E"; 
                    break; 
                case BlockType.Arrow:
                    code += "E";
                    break; 
                case BlockType.Axe:
                    code += "E";
                    break; 
                case BlockType.Spikes:
                    code += "E";
                    break; 
                case BlockType.Saw:
                    code += "E";
                    break; 
                case BlockType.FallingBlocks:
                    code += "E";
                    break; 
                case BlockType.RoadDark:
                    code += "R";
                    break; 
                case BlockType.RoadLight:
                    code += "R";
                    break; 
                case BlockType.River:
                    code += "I";
                    break; 
                case BlockType.MovingBlocks:
                    code += "M";
                    break; 
            }
        }

        code += "V"; 
        
        for (int i = 0; i < verticalBlocks.GetLength(0) - 1; i++)
        {
            switch (verticalBlocks[i, 0].GetComponent<BlockController>().blockType)
            {
                case BlockType.Empty:
                    code += "E";
                    break; 
                case BlockType.Plain1:
                    code += "E";
                    break; 
                case BlockType.Plain2:
                    code += "E";
                    break; 
                case BlockType.Plain3:
                    code += "E";
                    break; 
                case BlockType.Plain4:
                    code += "E";
                    break; 
                case BlockType.Plain5: 
                    code += "E"; 
                    break; 
                case BlockType.Arrow:
                    code += "E";
                    break; 
                case BlockType.Axe:
                    code += "E";
                    break; 
                case BlockType.Spikes:
                    code += "E";
                    break; 
                case BlockType.Saw:
                    code += "E";
                    break; 
                case BlockType.FallingBlocks:
                    code += "E";
                    break; 
                case BlockType.RoadDark:
                    code += "R";
                    break; 
                case BlockType.RoadLight:
                    code += "R";
                    break; 
                case BlockType.River:
                    code += "I";
                    break; 
                case BlockType.MovingBlocks:
                    code += "M";
                    break; 
            }
        }

        return code; 
    }

    private void GetBlocks()
    {
        horizontalBlocks = new GameObject[(int)size.z, (int)size.x];
        verticalBlocks = new GameObject[(int)size.y, (int)size.x];

        GameObject child;

        for (int i = 0; i < transform.childCount; i++)
        {
            child = transform.GetChild(i).gameObject;

            HandleChild(child);
        }
    }

    private bool CheckIfBlockIsRoad(GameObject block)
    {
        BlockController blockController = block.GetComponent<BlockController>();
        if (blockController.blockType == BlockType.RoadDark || blockController.blockType == BlockType.RoadLight)
        {
            return true; 
        }

        return false; 
    }

    private void HandleChild(GameObject child)
    {
        
        if (child.transform.localPosition.y == 0)
        {
            horizontalBlocks[(int)child.transform.localPosition.z, Math.Abs((int)child.transform.localPosition.x)] = child;
        }
        else
        {
            verticalBlocks[(int) child.transform.localPosition.y - 1, Math.Abs((int) child.transform.localPosition.x)] = child;
        }
        
        var childController = child.GetComponent<BlockController>();

        Vector3 height = new Vector3(0, child.transform.position.y, child.transform.position.z);
        if (childController.blockType == BlockType.River)
        {
            if (!riverCoordinates.Contains(height))
            {
                riverCoordinates.Add(height);
            }
        }
        else if (CheckIfBlockIsRoad(child))
        {
            if (!roadCoordinates.Contains(height))
            {
                roadCoordinates.Add(height);
            }
        }
    }

    public void SetSkin()
    {
        GameObject child; 
        for (int i = 0; i < transform.childCount; i++)
        {
            child = transform.GetChild(i).gameObject;

            var childController = child.GetComponent<BlockController>();

            childController.SetSkin("Winter");
        }
    }

    private Vector3 LeaveHeight(Vector3 vector)
    {
        return new Vector3(0, vector.y, vector.z);
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}