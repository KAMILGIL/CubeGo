using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LayerController : MonoBehaviour
{
    public string key;

    public List<PlatformController> platforms = new List<PlatformController>();
    
    private PlayerController playerController;
    private PrefabManager prefabManager;
    private EnvironmentController environmentController;

    private Vector3 visionVector = new Vector3(25, 22, 0);

    public Vector3 top;

    public void SetLayer(PlayerController playerController, PrefabManager prefabManager, Vector3 defaultPlatformPosition)
    {
        this.playerController = playerController;
        this.prefabManager = prefabManager;

        key = prefabManager.GetRandomKey();
        top = transform.position + prefabManager.GetSizeForKey(key) + Vector3.back;
        
        platforms.Add(CreatePlatform(defaultPlatformPosition));

        environmentController = GetComponent<EnvironmentController>();
        environmentController.SetEnvironmentController(platforms);
    }

    private void Update()
    {
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
        if (Math.Abs(platforms[index].transform.position.x - playerController.transform.position.x) >= visionVector.x)
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

        if (Math.Abs(leftPosition.x - playerController.transform.position.x) <= visionVector.x)
        {
            platforms.Insert(0, CreatePlatform(leftPosition));
        }
        
        if (Math.Abs(rightPosition.x - playerController.transform.position.x) <= visionVector.x)
        {
            platforms.Add(CreatePlatform(rightPosition));
        }
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
