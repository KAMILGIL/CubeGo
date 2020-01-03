using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class RiverController : MonoBehaviour
{
    public List<PlatformController> platforms = new List<PlatformController>();
    
    public List<GameObject> timbers = new List<GameObject>();

    private string[] timberNames = {"LargeTimber", "MediumTimber", "SmallTimber"}; 
    
    private List<GameObject> timberPrefabs = new List<GameObject>();

    private Vector3 speed;

    private PlayerController playerController; 

    public bool started = false;

    public void SetRiver(List<PlatformController> platforms, Vector3 speed, PlayerController playerController)
    {
        this.platforms = platforms;
        this.speed = speed;
        this.playerController = playerController;

        if (transform.localPosition.y > 0)
        {
            transform.localRotation = Quaternion.Euler(new Vector3(-90f, 0f, 0f));
        }

        foreach (string timberName in timberNames)
        {
            timberPrefabs.Add(Resources.Load<GameObject>("Enemies/Timbers/" + timberName));
        }
    }
    
    private void Update()
    {
        CheckTimbers(false);

        if (Mathf.Abs(playerController.transform.position.z - transform.position.z) > 6 
            || Mathf.Abs(playerController.transform.position.y - transform.position.y) > 6)
        {
            CheckTimbers(true);
            return;
        }

        FillRivers();
    }

    private void CheckTimbers(bool deleteAll)
    {
        int index = 0;
        while (true)
        {
            if (index >= timbers.Count || platforms.Count == 0)
            {
                break; 
            }

            if (timbers[index].transform.position.x < platforms.First().transform.position.x - 10 ||
                timbers[index].transform.position.x > platforms.Last().transform.position.x + 0 || 
                deleteAll)
            {
                Destroy(timbers[index]);
                timbers.RemoveAt(index);
            }
            else
            {
                index += 1; 
            }
        }
    }

    private void FillRivers()
    {
        Vector3 left = platforms.First().transform.position + Vector3.left * 10;
        Vector3 right = platforms.Last().transform.position + Vector3.right * 0;
        Vector3 position; 

        for (float i = left.x; i < right.x; i+=4f)
        {
            position = new Vector3(i, 0, 0);
            
            if (CheckPositionAbilityToSpawnTimber(position))
            {
                CreateTimber(position);
            }
        }
    }

    private bool CheckPositionAbilityToSpawnTimber(Vector3 position)
    {
        /* 
        RaycastHit rightHit, leftHit;

        if (!Physics.Raycast(position, Vector3.right, out rightHit, 6f) && 
            !Physics.Raycast(position, Vector3.left, out leftHit, 6f))
        {
            return true; 
        }

        return false;*/

        //if (Mathf.Abs(position.x - playerController.transform.position.x) < 8)
        //{
        //    return false;
        //}

        float distance = Random.Range(7f, 9f);

        foreach (GameObject timber in timbers)
        {
            if (Mathf.Abs(timber.transform.localPosition.x - position.x) < distance || Mathf.Abs(timber.transform.localPosition.x - position.x) < distance)
            {
                return false;
            }
        }

        return true;
    }

    private void CreateTimber(Vector3 position)
    {
        timbers.Add(Instantiate(timberPrefabs[Random.Range(0, timberPrefabs.Count)], position, Quaternion.identity));
        timbers.Last().GetComponent<TimberController>().speed = speed; 
        timbers.Last().transform.SetParent(transform, false);
    }
}
