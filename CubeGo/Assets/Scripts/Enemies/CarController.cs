using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public int carLength; // is from 2 to 4 i think, but depends on theme on other shit i fucked my fucking life 
    
    public Vector3 speed = Vector3.right * 2f; // is speed of the timber

    private GameObject skin, colliderPrefab;

    private List<GameObject> colliders = new List<GameObject>();

    private void Start()
    {
        colliderPrefab = Resources.Load<GameObject>("CustomColliders/BlockCollider");

        CreateBlocks("Winter");
    }

    private void Update()
    {
        transform.position += speed * Time.deltaTime;
    }

    private void CreateBlocks(string theme)
    {
        string carType; 
        
        switch (carLength)
        {
            case 2:
                carType = "SmallCar";
                break;
            case 3:
                carType = "MediumCar";
                break; 
            case 4:
                carType = "LargeCar";
                break;
            default:
                carType = "none";
                break;
        }

        skin = Instantiate(Resources.Load<GameObject>("Textures/" + theme + "/EnemySkins/" + carType), Vector3.zero, Quaternion.identity);
        skin.transform.SetParent(transform, false);

        for (int i = 0; i < carLength; i++)
        {
            colliders.Add(Instantiate(colliderPrefab, Vector3.right / 2 + Vector3.right * i + Vector3.left * carLength / 2, Quaternion.identity));
            colliders[i].transform.SetParent(transform, false);
            colliders[i].GetComponent<BlockColliderController>().speed = speed; 
        }
    }
}
