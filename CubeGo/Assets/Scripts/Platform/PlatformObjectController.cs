using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class PlatformObjectController : MonoBehaviour
{
    public List<Tuple<int, int>> horizontalRiverBlockIndexes, verticalRiverBlocksIndexes;
    
    public GameObject[,] horizontalBlocks, verticalBlocks;

    public float deltaTime;
    
    private string[] timberNames = new string[]{"MediumTimber", "LargeTimber", "SmallTimber"};

    public List<GameObject> timberPrefabs = new List<GameObject>();

    private void Start()
    {
        foreach (string name in timberNames)
        {
            timberPrefabs.Add(Resources.Load<GameObject>("Enemies/Timbers/" + name));
        }
    }

    private void Update()
    {
        deltaTime -= Time.deltaTime; 
    }

    private void CreateTimbers()
    {
        foreach (var index in horizontalRiverBlockIndexes)
        {
            bool canSpawn = true;
            for (int i = 0; i < 4; i++)
            {
                if (horizontalBlocks[index.Item1, index.Item2 - i].GetComponent<BlockController>().isCollising)
                {
                    canSpawn = false;
                }
            }

            if (canSpawn)
            {
                print(horizontalBlocks[index.Item1, index.Item2].transform.position);
                Instantiate(timberPrefabs[Random.Range(0, timberPrefabs.Count)],
                    horizontalBlocks[index.Item1, index.Item2].transform.position, Quaternion.identity);
            }
        }
    }

    public void RunManagement()
    {
        CreateTimbers();
    }
}