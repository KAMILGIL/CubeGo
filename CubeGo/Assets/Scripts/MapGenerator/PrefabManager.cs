using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class PrefabManager : MonoBehaviour
{
    public List<GameObject> prefabs;

    public Dictionary<string, List<int>> data = new Dictionary<string, List<int>>();
    public List<string> keys = new List<string>();

    private string[] names = new string[]{"P(10-4-6)", "P(10-10-10)Cars"};

    private void LoadPrefabs()
    {
        foreach (string name in names)
        {
            prefabs.Add(Resources.Load<GameObject>("MapPrefabs/Platforms/" + name));
        }
    }

    public void HandlePrefabs()
    {
        LoadPrefabs();
        for (int i = 0; i < prefabs.Count; i++)
        {
            HandlePrefab(i);
        }
    }

    private void HandlePrefab(int index)
    {
        string key = prefabs[index].GetComponent<PlatformController>().GetPlatformCode();
        
        if (!keys.Contains(key)) 
        {
            data[key] = new List<int>();
            keys.Add(key);
        }
        
        data[key].Add(index);
    }

    public GameObject GetPrefab(string code) // if is equal to 'Any' returns random prefab 
    {
        string key = code;
        return prefabs[data[key][0]]; // might be not only zero 
    }

    public string GetRandomKey()
    {
        return keys[Random.Range(0, keys.Count)];
    }

    public Vector3 GetSizeForKey(string key)
    {
        PlatformController platformController = prefabs[data[key][0]].GetComponent<PlatformController>();
        return new Vector3(0, platformController.size.y, platformController.size.z);
    }
}