using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TimberController : MonoBehaviour
{
    public int timberLength;

    public GameObject platform;

    public Vector3 speed = Vector3.right * 2f; // is speed of the timber

    private GameObject skin, colliderPrefab;

    private List<GameObject> colliders = new List<GameObject>();

    private void Start()
    {
        colliderPrefab = Resources.Load<GameObject>("CustomColliders/BlockCollider"); 
        print("colliderPrefab" + colliderPrefab.ToString());
        
        CreateBlocks("Winter");
    }

    private void Update()
    {
        transform.position += speed * Time.deltaTime;
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
                timberType = "none";
                break;
        }
        
        
        skin = Instantiate(Resources.Load<GameObject>("Textures/" + theme + "/EnemySkins/" + timberType), Vector3.zero, Quaternion.identity);
        skin.transform.SetParent(transform, false);

        for (int i = 0; i < timberLength; i++)
        {
            colliders.Add(Instantiate(colliderPrefab, Vector3.right * i, Quaternion.identity));
            colliders[i].transform.SetParent(transform, false);
            colliders[i].GetComponent<BlockColliderController>().platform = platform;
            colliders[i].GetComponent<BlockColliderController>().speed = speed; 
        }
    }
}