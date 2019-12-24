using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;
using UnityEditor;
using Random = UnityEngine.Random;

public class PrefabManager : MonoBehaviour
{
    public List<GameObject> prefabs;

    public Dictionary<string, List<int>> data = new Dictionary<string, List<int>>();
    public List<string> keys = new List<string>();

    private void LoadPrefabs()
    {
        UnityEngine.Object prefab = AssetDatabase.LoadAssetAtPath("Assets/Objects/MapPrefab/Platform.prefab", typeof(GameObject)); 
        prefabs.Add(prefab as GameObject);
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
        string key = prefabs[index].GetComponent<PlatformController>().GetData();
        if (!data.ContainsKey(key)) 
        {
            data[key] = new List<int>();
        }
        
        data[key].Add(index);
        keys.Add(key);
    }

    public GameObject GetPrefab(string conditions) // if is equal to 'Any' returns random prefab 
    {
        if (conditions == "Any")
        {
            return prefabs[data[keys[Random.Range(0, keys.Count - 1)]][0]];
        }

        return prefabs[data["0"][0]];
    }
}