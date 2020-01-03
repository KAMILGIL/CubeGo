using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;

public class LayerController : MonoBehaviour
{
    public string key;

    public List<PlatformController> platforms = new List<PlatformController>();
    
    private PlayerController playerController;
    private PrefabManager prefabManager;
    private EnvironmentController environmentController;

    private Vector3 visionVector = new Vector3(10, 12, 0), currentRow;

    public Vector3 top;

    public void SetLayer(PlayerController playerController, PrefabManager prefabManager, Vector3 defaultPlatformPosition)
    {
        this.playerController = playerController;
        this.prefabManager = prefabManager;

        key = prefabManager.GetRandomKey();
        top = transform.position + prefabManager.GetSizeForKey(key) + Vector3.back;
        
        platforms.Add(CreatePlatform(defaultPlatformPosition));

        environmentController = GetComponent<EnvironmentController>();
        environmentController.SetEnvironmentController(platforms, playerController);
    }

    private void Update()
    {
        currentRow = FindPlayerRowCoordinates();
        CheckPlatformAtIndex(0);
        CheckPlatformAtIndex(platforms.Count - 1);
        AddSidePlatforms();
    }

    private void CheckPlatformAtIndex(int index) 
    {
        if (platforms.Count == 0)
        {
            return;
        }

        Vector3 fromTarget = Vector3.zero;
        if (true)
        {
            fromTarget = currentRow; 
        }
        
        if (Math.Abs(platforms[index].transform.position.x - fromTarget.x) > visionVector.x)
        {
            platforms[index].DestroySelf();
            platforms.RemoveAt(index);
        }
    }

    private void AddSidePlatforms()
    {
        if (platforms.Count == 0)
        {
            return;
        }
        Vector3 leftPosition = platforms[0].transform.localPosition + Vector3.left * 10;
        Vector3 rightPosition = platforms[platforms.Count - 1].transform.localPosition + Vector3.right * 10;

        Vector3 fromTarget = Vector3.zero; 

        if (true)
        {
            fromTarget = currentRow; 
        }

        if (Math.Abs(leftPosition.x - fromTarget.x) <= visionVector.x)
        {
            platforms.Insert(0, CreatePlatform(leftPosition));
        }
        
        if (Math.Abs(rightPosition.x - fromTarget.x) <= visionVector.x)
        {
            platforms.Add(CreatePlatform(rightPosition));
        }
    }

    private Vector3 FindPlayerRowCoordinates()
    {
        foreach (PlatformController platform in platforms)
        {
            if (playerController.transform.position.x >= platform.transform.position.x - 10
                && playerController.transform.position.x <= platform.transform.position.x)
            {
                return new Vector3(platform.transform.position.x, 0, 0);
            }
        }

        return Vector3.zero;
    }

    private PlatformController CreatePlatform(Vector3 position)
    {
        GameObject platformPrefab = prefabManager.GetPrefab(key);
        PlatformController platformController = Instantiate(platformPrefab, position, Quaternion.identity).GetComponent<PlatformController>();
        
        platformController.transform.SetParent(transform, false);
        platformController.layer = this;
        
        return platformController;
    }
}
