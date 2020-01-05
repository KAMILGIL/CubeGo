using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnvironmentController : MonoBehaviour
{
    private List<PlatformController> platforms;
    private PlayerController playerController; 

    private List<RiverController> rivers = new List<RiverController>();
    private List<RoadController> roads = new List<RoadController>();
    private List<RollController> rolls = new List<RollController>();

    private GameObject riverPrefab, rollPrefab, roadPrefab;

    public void SetEnvironmentController(List<PlatformController> platforms, PlayerController playerController)
    {
        this.platforms = platforms;
        this.playerController = playerController;

        riverPrefab = Resources.Load<GameObject>("Objects/River");
        rollPrefab = Resources.Load<GameObject>("Objects/Roll");
        roadPrefab = Resources.Load<GameObject>("Objects/Road");

        foreach (Vector3 height in platforms.First().riverCoordinates)
        {
            rivers.Add(Instantiate(riverPrefab, height, Quaternion.identity).GetComponent<RiverController>());
            rivers.Last().transform.SetParent(transform, false);
            rivers.Last().SetRiver(this.platforms, GetRandomRiverSpeed(), playerController);
        }

        foreach (Vector3 height in platforms.First().roadCoordinates)
        {
            roads.Add(Instantiate(roadPrefab, height, Quaternion.identity).GetComponent<RoadController>());
            roads.Last().transform.SetParent(transform, false); 
            roads.Last().SetRoad(this.platforms, GetRandomRoadSpeed(), playerController);
        }

        print(platforms.First().rollBegins.Count.ToString() + " fuck this shit ");
        print(platforms.First().rollData.Count.ToString() + "   " + platforms.First().name);
        platforms.First().SetRolls();
        foreach (Roll rollData in platforms.First().rollData)
        {
            print("setting roll");
            rolls.Add(Instantiate(rollPrefab, rollData.center, Quaternion.identity).GetComponent<RollController>());
            rolls.Last().transform.SetParent(transform, false);
            rolls.Last().SetRoll(rollData.center, rollData.length, playerController, platforms);
        }
    }
    
    private Vector3 GetRandomRiverSpeed()
    {
        if (Random.Range(0, 2) == 0)
        {
            return Vector3.right * Random.Range(0.8f, 4.5f);
        }
        return Vector3.left * Random.Range(0.8f, 4.5f);
    }

    private Vector3 GetRandomRoadSpeed()
    {
        if (Random.Range(0, 2) == 0)
        {
            return Vector3.right * Random.Range(0.8f, 4.5f);
        }
        return Vector3.left * Random.Range(0.8f, 4.5f);
    }
}

public class VectorHandler
{
    public Vector3 LeaveOnlyHeight(Vector3 vector)
    {
        return new Vector3(0, vector.y, vector.z);
    }
}