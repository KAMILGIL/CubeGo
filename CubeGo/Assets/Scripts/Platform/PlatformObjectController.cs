using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class PlatformObjectController : MonoBehaviour
{
    public List<Tuple<int, int>> horizontalRiverBlockIndexes = new List<Tuple<int, int>>(), verticalRiverBlocksIndexes;
    
    public GameObject[,] horizontalBlocks, verticalBlocks;

    public List<GameObject> timbers = new List<GameObject>();

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
            bool canSpawn;
            RaycastHit hit; 
            
            if (Physics.Raycast(horizontalBlocks[index.Item1, index.Item2].transform.position, Vector3.right, out hit, Random.Range(8, 15)))
            {
                canSpawn = false; 
            }
            else
            {
                canSpawn = true; 
            }

            if (canSpawn)
            {
                print(horizontalBlocks[index.Item1, index.Item2].transform.position);
                GameObject timber = Instantiate(timberPrefabs[Random.Range(0, timberPrefabs.Count)],
                    horizontalBlocks[index.Item1, index.Item2].transform.position, Quaternion.identity);
                timber.GetComponent<TimberController>().platform = gameObject;
            }
        }
    }

    public void RunManagement()
    {
        CreateTimbers();
    }
}