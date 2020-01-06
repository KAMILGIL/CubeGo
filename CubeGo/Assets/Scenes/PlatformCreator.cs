using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCreator : MonoBehaviour
{
    private GameObject blockPrefab, platformPrefab;

    private void Start()
    {
        blockPrefab = Resources.Load<GameObject>("MapPrefabs/Blocks/Block");
        platformPrefab = Resources.Load<GameObject>("MapPrefabs/Platforms/Platform");
        
        CreatePlatform(10, 5, 5);
    }

    private void CreatePlatform(int width, int length, int height)
    {
        GameObject platform = Instantiate(platformPrefab, Vector3.zero, Quaternion.identity);
        GameObject block;
        platform.GetComponent<PlatformController>().isInGame = false;
        
        for (int j = 0; j < length; j++)
        {
            for (int i = 0; i < width; i++)
            {
                block = Instantiate(blockPrefab, new Vector3(-i, 0, j), Quaternion.identity);
                block.transform.SetParent(platform.transform);
            }
        }

        for (int j = 0; j < height; j++)
        {
            for (int i = 0; i < width; i++)
            {
                block = Instantiate(blockPrefab, new Vector3(-i, j + 1, length - 1), Quaternion.Euler(new Vector3(270f, 0, 0)));
                block.transform.SetParent(platform.transform);
                if (j == height - 1)
                {
                    block.GetComponent<BlockController>().blockType = BlockType.Edge;
                }
            }
        }
        
        platform.GetComponent<PlatformController>().size = new Vector3(width, height, length);
    }
}
