//using System;

using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MapGenerator : MonoBehaviour
{
    public GameObject[] platformPrefabs;

    public PlayerController playerController;

    private List<Layer> layers = new List<Layer>();
    
    private int visibleBlocks = 1;
    private Vector3 visionVector = new Vector3(24, 0, 0);

    private void Start()
    {
        layers.Add(new Layer(Vector3.zero));
        layers[0].AddPlatform(CreatePlatform(platformPrefabs[0], Vector3.zero));
    }

    private void CreateNewLayer()
    {
        
    }

    private void AddRightPlatforms()
    {
        Vector3 newPosition; 
        print("adding right platforms");
        
        for (int layerIndex = 0; layerIndex < layers.Count; layerIndex++)
        {
            newPosition = layers[layerIndex].GetLastPlatformPosition() + Vector3.right * 18;
            if (Math.Abs(newPosition.x) >
                Math.Abs(playerController.currentPlatform.transform.position.x) + visionVector.x)
            {
                return;
            }

            if (Math.Abs(layers[layerIndex].GetFirstPlatformPosition().x - playerController.currentPlatform.transform.position.x) > visionVector.x)
            {
                layers[layerIndex].DeleteFirstPlatform();
                print("deleting the first");
            }
            
            layers[layerIndex].AddPlatform(CreatePlatform(platformPrefabs[0], newPosition));
        }
    }

    public void MovedRight(GameObject platform)
    {
        AddRightPlatforms();
    }

    public void MovedLeft(GameObject platform)
    {
    } 

    public void MovedForward(GameObject platform)
    {
    }

    public void MovedBackward(GameObject platform)
    {
        
    }

    private GameObject CreatePlatform(GameObject prefab, Vector3 position)
    {
        return Instantiate(prefab, position, Quaternion.identity);
    }
}

/* 
public class MapGenerator : MonoBehaviour
{
    public GameObject[] platformPrefabs;

    public PlayerController playerController;

    private List<Layer> layers = new List<Layer>();
    
    private int visibleBlocks = 1;
    private Vector3 visionVector = Vector3.up * 10; 
    
    private void Start()
    {
        layers.Add(new Layer(Vector3.zero));
        print(layers.Count);
        layers[0].platforms.Add(CreatePlatform(platformPrefabs[0], Vector3.zero));
        
        for (int i = 1; i < visibleBlocks; i++)
        {
            //CreateNewLayer(Vector3.zero);
        }
    }

    private Vector3 GetDeltaVectorFromPlatformPrefab(GameObject platformPrefab)
    {
        return GetDeltaVectorFromCornerController(platformPrefab.GetComponent<CornerController>());
    }

    private Vector3 GetDeltaVectorFromCornerController(CornerController controllerController)
    {
        return new Vector3(0, controllerController.height, controllerController.length - 1);
    }

    private void CreateNewLayer(Vector3 currentX, int currentLayer)
    {
        var layerPlatformType = Random.Range(0, 0);
        Vector3 layerHeight, deltaBetweenPlayerAndNewLayer; 

        for (int i = 0; i < visibleBlocks; i++)
        {
            layerHeight = layers[layers.Count - 1].height + GetDeltaVectorFromPlatformPrefab(platformPrefabs[layerPlatformType]);
            deltaBetweenPlayerAndNewLayer = layerHeight - playerController.currentPlatform.transform.position;
            if (deltaBetweenPlayerAndNewLayer.magnitude > visionVector.magnitude)
            {
                return;
            }
            
            layers.Add(new Layer(layerHeight));
            layers[layers.Count - 1].platforms
                .Add(CreatePlatform(platformPrefabs[layerPlatformType], new Vector3(currentX.x, 0, 0) + layers[layers.Count - 1].height));
        }
    }

    private void AddRightPlatforms(Vector3 currentX)
    {
        for (int layerIndex = 0; layerIndex < layers.Count; layerIndex++)
        {
            // prefab needs to be checked
            var position = layers[layerIndex].platforms[layers[layerIndex].platforms.Count - 1].transform.position + Vector3.right * 18f;
            if (position.x - currentX.x > visibleBlocks * Vector3.right.x * 18f)
            {
                return; 
            }
            layers[layerIndex].platforms.Add(CreatePlatform(platformPrefabs[0], position));
        }
    }

    private void AddLeftPlatforms(Vector3 currentX)
    {
        for (int layerIndex = 0; layerIndex < layers.Count; layerIndex++)
        {
            // prefab needs to be checked
            var position = layers[layerIndex].platforms[0].transform.position - Vector3.right * 18f;
            if (System.Math.Abs(position.x - currentX.x) > visibleBlocks * Vector3.right.x * 18f)
            {
                return; 
            }
            layers[layerIndex].platforms.Insert(0, CreatePlatform(platformPrefabs[0], position));
        }
    }

    public void MovedRight(GameObject platform)
    {
        AddRightPlatforms(platform.transform.position);
    }

    public void MovedLeft(GameObject platform)
    {
        AddLeftPlatforms(platform.transform.position);
    } 

    public void MovedForward(GameObject platform)
    {
        CreateNewLayer(platform.transform.position, 0);
    }

    public void MovedBackward(GameObject platform)
    {
        
    }
    
    private GameObject CreatePlatform(GameObject prefab, Vector3 position)
    {
        return Instantiate(prefab, position, Quaternion.Euler(Vector3.zero));
    }
}
*/ 

public class Layer
{
    public Vector3 height;
    public PlatformType horizontalType, verticalType;
    public List<GameObject> platforms = new List<GameObject>(); 

    public Layer(Vector3 height)
    {
        this.height = height; 
    }

    public void AddPlatform(GameObject platform)
    {
        this.platforms.Add(platform);
    }

    public void DeleteLastPlatform()
    {
        if (platforms.Count == 0)
        {
            return;
        }
        
        platforms.Last().GetComponent<CornerController>().DestroySelf();
        platforms.RemoveAt(platforms.Count - 1);
    }
    
    public void DeleteFirstPlatform()
    {
        if (platforms.Count == 0)
        {
            return;
        }
        
        platforms.First().GetComponent<CornerController>().DestroySelf();
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