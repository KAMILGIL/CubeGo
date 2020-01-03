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

    private List<LayerController> layers = new List<LayerController>();
    
    private Vector3 downVision = new Vector3(0, 11, 0), upVision = new Vector3(0, 15, 0);

    private GameObject layerPrefab;
    private PlatformController currentPlatform;

    private void Start()
    {
        prefabManager = GetComponent<PrefabManager>();
        prefabManager.HandlePrefabs();

        layerPrefab = Resources.Load<GameObject>("Objects/Layer");
    }

    private void Update()
    {
        int layersToDestroyAmount = 0;
        currentPlatform = FincCurrentPlatform();
        
        for (int i = 0; i < layers.Count; i++)
        {
            if (CheckLayerAtIndex(i))
            {
                layersToDestroyAmount += 1;
            }
            else
            {
                break;
            }
        }

        for (int i = 0; i < layersToDestroyAmount; i++)
        {
            layers.RemoveAt(0);
        }

        while (true)
        {
            if (Math.Abs(layers[layers.Count - 1].top.y - playerController.transform.position.y) <= upVision.y)
            {
                layers.Add(CreateLayer());
            }
            else
            {
                break;
            }
        }
    }

    private PlatformController FincCurrentPlatform()
    {
        PlatformController platformController; 
        
        for (int i = 0; i < layers.Count; i++)
        {
            for (int j = 0; j < layers[i].platforms.Count; j++)
            {
                platformController = layers[i].platforms[j];
                
                if (CheckPlayerOnPlatform(platformController))
                {
                    return platformController;
                }
            }
        }

        return null; 
    }

    private bool CheckLayerAtIndex(int index)
    {
        if (Math.Abs(layers[index].transform.position.y - playerController.transform.position.y) > downVision.y)
        {
            Destroy(layers[index].gameObject);
            return true; 
        }

        return false;
    }

    public void InitMap()
    {
        layers.Add(CreateLayer());
        layers.Add(CreateLayer());
    }

    private LayerController CreateLayer()
    {
        Vector3 position = Vector3.zero;
        if (layers.Count != 0)
        {
            position = layers[layers.Count - 1].top;
        }
        
        GameObject layer = Instantiate(layerPrefab, position, Quaternion.identity);
        LayerController layerController = layer.GetComponent<LayerController>(); 
        
        layerController.SetLayer(playerController, prefabManager, GetCurrentSpawnPosition());
        return layerController;
    }

    private bool CheckPlayerOnPlatform(PlatformController platformController)
    {
        Vector3 platformPosition = platformController.transform.position;
        Vector3 playerPosition = playerController.transform.position;
        Vector3 platformSize = platformController.size;

        if (playerPosition.x <= platformPosition.x && playerPosition.x + 10 >= platformPosition.x && 
            playerPosition.y >= platformPosition.y && playerPosition.y <= platformPosition.y + platformSize.y)
        {
            return true; 
        }

        return false;
    }

    private Vector3 GetCurrentSpawnPosition()
    {
        if (currentPlatform)
        {
            return new Vector3(currentPlatform.transform.localPosition.x, 0, 0);
        }

        return Vector3.zero;
    }
}