using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Random = UnityEngine.Random;

public class RoadController : MonoBehaviour
{
    public List<PlatformController> platforms = new List<PlatformController>();
    
    public List<GameObject> cars = new List<GameObject>();

    private string[] carNames = {"LargeCar", "MediumCar", "SmallCar"}; 
    
    private List<GameObject> carPrefabs = new List<GameObject>();

    private Vector3 speed;

    private PlayerController playerController;

    public void SetRoad(List<PlatformController> platforms, Vector3 speed, PlayerController playerController)
    {
        this.platforms = platforms;
        this.speed = speed;
        this.playerController = playerController;
        
        if (transform.localPosition.y > 0)
        {
            transform.localRotation = Quaternion.Euler(new Vector3(-90f, 0f, 0f));
        }
        
        foreach (string timberName in carNames)
        {
            carPrefabs.Add(Resources.Load<GameObject>("Enemies/Cars/" + timberName));
        }
    }

    private void Update()
    {
        CheckCars(false);
        
        if (Mathf.Abs(transform.position.z - playerController.transform.position.z) > 6
            || Mathf.Abs(playerController.transform.position.y - transform.position.y) > 6)
        {
            print("road disabled");
            CheckCars(true);
            return;
        }
        
        FillRoad();
    }

    private void CheckCars(bool deleteAll)
    {
        int index = 0;
        while (true)
        {
            if (index >= cars.Count || platforms.Count == 0)
            {
                break; 
            }

            if (cars[index].transform.position.x < platforms.First().transform.position.x - 10 ||
                cars[index].transform.position.x > platforms.Last().transform.position.x || 
                deleteAll)
            {
                Destroy(cars[index]);
                cars.RemoveAt(index);
            }
            else
            {
                index += 1; 
            }
        }
    }

    private void FillRoad()
    {
        Vector3 left = platforms.First().transform.position + Vector3.left * 10;
        Vector3 right = platforms.Last().transform.position;
        Vector3 position; 

        for (float i = left.x; i < right.x; i+=Random.Range(8f, 15))
        {
            position = new Vector3(i, 0, 0) + Vector3.up * 0.8f;
            
            if (CheckPositionAbilityToSpawnCar(position))
            {
                CreateCar(position);
            }
        }
    }

    private bool CheckPositionAbilityToSpawnCar(Vector3 position)
    {
        //if (Mathf.Abs(position.x - playerController.transform.position.x) < 4)
        //{
        //    return false;
        //}

        foreach (GameObject car in cars)
        {
            if (Mathf.Abs(car.transform.position.x - position.x) < 9)
            {
                return false; 
            }
        }

        return true; 
        
        
        /* 
        RaycastHit rightHit, leftHit;

        if (!Physics.Raycast(position, Vector3.right, out rightHit,6f) && 
            !Physics.Raycast(position, Vector3.left, out leftHit, 6f))
        {
            return true; 
        }*/ 

        return false;
    }

    private void CreateCar(Vector3 position)
    {
        cars.Add(Instantiate(carPrefabs[Random.Range(0, carPrefabs.Count)], position, Quaternion.identity));
        cars.Last().GetComponent<CarController>().speed = speed; 
        cars.Last().transform.SetParent(transform, false);
    }
}
