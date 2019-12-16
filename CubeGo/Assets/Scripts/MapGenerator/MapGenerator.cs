using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject platformPrefab;

    private void Start()
    {
        Instantiate(platformPrefab, Vector3.zero, Quaternion.identity);
        Instantiate(platformPrefab, Vector3.forward * 4 + Vector3.up * 2, Quaternion.Euler(Vector3.right * 90f));
    }
}
