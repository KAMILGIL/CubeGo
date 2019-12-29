using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimberController : MonoBehaviour
{
    public int timberLength;

    private GameObject skin;

    private List<GameObject> colliders = new List<GameObject>(); 
    
    private void Start()
    {
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

        var collider = gameObject.AddComponent<BoxCollider>();
        collider.transform.localScale = new Vector3(timberLength, 1, 1);
        collider.transform.position = Vector3.right * timberLength; 
    }
}