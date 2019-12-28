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

    private string[] names = new string[]{"P(10-4-6)", "P(10-7-8)", "P(10-10-10)", "P(10-10-10)Cars"};

    private void LoadPrefabs()
    {
        print(names);
        foreach (string name in names)
        {
            prefabs.Add(Resources.Load<GameObject>("MapPrefabs/Platforms/" + name));
        }
        print(prefabs.Count);
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
        prefabs[index].GetComponent<PlatformController>().SetPlatformData();
        string key = prefabs[index].GetComponent<PlatformController>().platformData.ToString();
        
        if (!keys.Contains(key)) 
        {
            data[key] = new List<int>();
            keys.Add(key);
        }
        
        data[key].Add(index);
    }

    public GameObject GetPrefab(PlatformData conditions) // if is equal to 'Any' returns random prefab 
    {
        if (conditions.isAny) // any size and any types of both block arrays 
        {
            string randomKey = keys[Random.Range(0, keys.Count)];
            return prefabs[data[randomKey][0]]; // must be random index, not only zero 
        }
        // give other platform with same conditions
        
        string key = conditions.ToString();
        return prefabs[data[key][0]]; // might be not only zero 
    }
}