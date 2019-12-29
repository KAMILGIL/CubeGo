using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimberController : MonoBehaviour
{
    public int timberLength;

    private GameObject colliderPrefab, skin;

    private List<GameObject> colliders = new List<GameObject>(); 
    
    private void Start()
    {
        colliderPrefab = Resources.Load<GameObject>("Objects/PlayerCollider");
        print(colliderPrefab);
        CreateBlocks("Winter");
    }

    private void Update()
    {
        transform.position += Vector3.right * 0.4f * Time.deltaTime;
    }

    private void CreateBlocks(string theme)
    {
        string timberType; 
        
        switch (timberLength)
        {
            case 3:
                timberType = "SmallTimber";
                break;
            case 4:
                timberType = "MediumTimber";
                break; 
            case 5:
                timberType = "LargeTimber";
                break;
            default:
                timberType = "SmallTimber";
                break;
        }
        
        
        skin = Instantiate(Resources.Load<GameObject>("Textures/" + theme + "/EnemySkins/" + timberType), Vector3.zero, Quaternion.identity);
        skin.transform.SetParent(transform, false);
        
        
        for (int i = 0; i < timberLength; i++)
        {
            colliders.Add(Instantiate(colliderPrefab, Vector3.right * i + Vector3.left * i * 0.2f, Quaternion.identity));
            colliders[i].transform.SetParent(transform, false);
        }
    }
}