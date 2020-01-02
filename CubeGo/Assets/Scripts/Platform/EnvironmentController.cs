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

    private List<RiverController> rivers = new List<RiverController>();
    private List<RoadController> roads = new List<RoadController>();

    private GameObject riverPrefab, rollPrefab, roadPrefab;

    public void SetEnvironmentController(List<PlatformController> platforms)
    {
        this.platforms = platforms;

        riverPrefab = Resources.Load<GameObject>("Objects/River");
        rollPrefab = Resources.Load<GameObject>("Objects/Roll");
        roadPrefab = Resources.Load<GameObject>("Objects/Road");

        foreach (Vector3 height in platforms.First().riverCoordinates)
        {
            rivers.Add(Instantiate(riverPrefab, height, Quaternion.identity).GetComponent<RiverController>());
            rivers.Last().transform.SetParent(transform, false);
            rivers.Last().SetRiver(this.platforms, GetRandomRiverSpeed());
        }

        foreach (Vector3 height in platforms.First().roadCoordinates)
        {
            roads.Add(Instantiate(roadPrefab, height, Quaternion.identity).GetComponent<RoadController>());
            roads.Last().transform.SetParent(transform, false); 
            roads.Last().SetRoad(this.platforms, GetRandomRoadSpeed());
        }
    }

    private void Update()
    {
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