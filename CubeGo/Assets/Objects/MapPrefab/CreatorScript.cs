using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatorScript : MonoBehaviour
{
    public GameObject cubePrefab; 
    
    private void Start()
    {
        for (int j = 0; j < 10; j++)
        {
            for (int i = 0; i < 24; i++)
            {
                Instantiate(cubePrefab, new Vector3(-i, 0, j), Quaternion.identity);
            }
        }
        
    }
}
