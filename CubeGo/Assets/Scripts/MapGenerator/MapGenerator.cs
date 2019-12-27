using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MapGenerator : MonoBehaviour
{
    private PrefabManager prefabManager; 

    public PlayerController playerController;

    private List<Layer> layers = new List<Layer>();
    
    private int visibleBlocks = 1;
    private Vector3 visionVector = new Vector3(23, 12, 0);

    private void Start()
    {
        prefabManager = GetComponent<PrefabManager>();
        prefabManager.HandlePrefabs();
        var newPlatformPrefabController = prefabManager.GetPrefab(new PlatformData(true)).GetComponent<PlatformController>();

        layers.Add(new Layer(Vector3.zero, newPlatformPrefabController.platformData));
        layers[0].AddPlatform(CreatePlatform(prefabManager.GetPrefab(newPlatformPrefabController.platformData), Vector3.zero));
    }

    public void InitMap()
    {
        playerController.currentPlatform = layers[0].platforms[0];
        CreateNewLayer(layers[0].platforms[0]);
        AddRightPlatforms();
        AddRightPlatforms();
        AddLeftPlatforms();
        AddLeftPlatforms();
    }

    private void Update()
    {
        int currentLayer = FindCurrentLayer(playerController.currentPlatform), distantLayersAmount = 0;

        for (int layerIndex = 0; layerIndex < layers.Count; layerIndex++)
        {
            if (Math.Abs(layers[layerIndex].height.y - playerController.transform.position.y) > visionVector.y)
            {
                layers[layerIndex].DeleteAllPlatforms();
                distantLayersAmount += 1;
            }
            else
            {
                break;
            }
        }

        for (int i = 0; i < distantLayersAmount; i++)
        {
            layers.RemoveAt(i);
        }
    }

    private void CreateNewLayer(GameObject currentPlatform)
    {
        Vector3 newLayerCenter;
        int currentLayer = FindCurrentLayer(currentPlatform), distantLayersAmount = 0;

        for (int layerIndex = 0; layerIndex < layers.Count; layerIndex++)
        {
            if (Math.Abs(layers[layerIndex].height.y - layers[currentLayer].height.y) > visionVector.y)
            {
                layers[layerIndex].DeleteAllPlatforms();
                distantLayersAmount += 1;
            }
            else
            {
                break;
            }
        }

        for (int i = 0; i < distantLayersAmount; i++)
        {
            layers.RemoveAt(i);
        }

        while (true)
        {
            if (Math.Abs(layers.Last().height.y - currentPlatform.transform.position.y) > visionVector.y)
            {
                return;
            }

            GameObject newPlatformPrefab = prefabManager.GetPrefab(new PlatformData(true));
            PlatformController itsController = newPlatformPrefab.GetComponent<PlatformController>();
            layers.Add(new Layer(layers.Last().height + GetDeltaVectorFromPlatformPrefab(layers.Last().platforms[0]), itsController.platformData));
            layers.Last().platforms.Add(CreatePlatform(prefabManager.GetPrefab(itsController.platformData), layers.Last().height + new Vector3(currentPlatform.transform.position.x, 0, 0)));
            AddRightPlatforms();
            AddLeftPlatforms();
        }
    }
    
    private Vector3 GetDeltaVectorFromPlatformPrefab(GameObject platformPrefab)
    {
        return GetDeltaVectorFromCornerController(platformPrefab.GetComponent<PlatformController>());
    }

    private Vector3 GetDeltaVectorFromCornerController(PlatformController controllerController)
    {
        return new Vector3(0, controllerController.size.y, controllerController.size.z - 1);
    }

    private int FindCurrentLayer(GameObject platform)
    {
        Vector3 platformHeight = new Vector3(x: 0, y: platform.transform.position.y, z: platform.transform.position.z);
        for (int layerIndex = 0; layerIndex < layers.Count; layerIndex++)
        {
            if (platformHeight.Compare(layers[layerIndex].height, 1))
            {
                return layerIndex;
            }
        }

        return 0;
    }

    private void AddRightPlatforms()
    {
        Vector3 newPosition; 
        
        for (int layerIndex = 0; layerIndex < layers.Count; layerIndex++)
        {
            newPosition = layers[layerIndex].GetLastPlatformPosition() + Vector3.right * 10;
            if (Math.Abs(newPosition.x - playerController.currentPlatform.transform.position.x) > visionVector.x)
            {
                continue;
            }
            layers[layerIndex].AddPlatform(CreatePlatform(prefabManager.GetPrefab(layers[layerIndex].platformData), newPosition));

            if (Math.Abs(layers[layerIndex].GetFirstPlatformPosition().x - playerController.currentPlatform.transform.position.x) > visionVector.x)
            {
                layers[layerIndex].DeleteFirstPlatform();
            }
        }
    }
    
    private void AddLeftPlatforms()
    {
        Vector3 newPosition; 
        
        for (int layerIndex = 0; layerIndex < layers.Count; layerIndex++)
        {
            newPosition = layers[layerIndex].GetFirstPlatformPosition() + Vector3.left * 10;
            if (Math.Abs(newPosition.x - playerController.currentPlatform.transform.position.x) > visionVector.x)
            {
                continue;
            }
            layers[layerIndex].InsertPlatform(CreatePlatform(prefabManager.GetPrefab(layers[layerIndex].platformData), newPosition));
            
            if (Math.Abs(layers[layerIndex].GetLastPlatformPosition().x - playerController.currentPlatform.transform.position.x) > visionVector.x)
            {
                layers[layerIndex].DeleteLastPlatform();
            }
        }
    }

    public void MovedRight(GameObject platform)
    {
        AddRightPlatforms();
    }

    public void MovedLeft(GameObject platform)
    {
        AddLeftPlatforms();
    } 

    public void MovedForward(GameObject platform)
    {
        CreateNewLayer(platform);
    }

    public void MovedBackward(GameObject platform)
    {
        
    }

    private GameObject CreatePlatform(GameObject prefab, Vector3 position)
    {
        return Instantiate(prefab, position, Quaternion.identity);
    }
}

public class Layer
{
    public Vector3 height;
    public PlatformData platformData; // like "10-7-8-Common-Common"
    public List<GameObject> platforms = new List<GameObject>(); 

    public Layer(Vector3 height, PlatformData platformData)
    {
        this.height = height;
        this.platformData = platformData;
    }

    public void AddPlatform(GameObject platform)
    {
        this.platforms.Add(platform);
    }

    public void InsertPlatform(GameObject platform)
    {
        this.platforms.Insert(0, platform);
    }

    public void DeleteLastPlatform()
    {
        if (platforms.Count == 0)
        {
            return;
        }
        
        platforms.Last().GetComponent<PlatformController>().DestroySelf();
        platforms.RemoveAt(platforms.Count - 1);
    }

    public void DeleteAllPlatforms()
    {
        int count = platforms.Count;
        
        for (int i = 0; i < count; i++)
        {
            platforms[0].GetComponent<PlatformController>().DestroySelf();
            platforms.RemoveAt(0);
        }
    }
    
    public void DeleteFirstPlatform()
    {
        if (platforms.Count == 0)
        {
            return;
        }
        
        platforms.First().GetComponent<PlatformController>().DestroySelf();
        platforms.RemoveAt(0);
    }

    public Vector3 GetFirstPlatformPosition()
    {
        if (platforms.Count == 0)
        {
            return Vector3.zero;
        }
        
        return platforms[0].transform.position;
    }
    
    public Vector3 GetLastPlatformPosition()
    {
        if (platforms.Count == 0)
        {
            return Vector3.zero;
        }

        return platforms[platforms.Count - 1].transform.position;
    }

    public void GetPlatformInfo()
    {
        
    }
} 