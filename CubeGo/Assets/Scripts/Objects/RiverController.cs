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

    public void SetRiver(List<PlatformController> platforms, Vector3 speed)
    {
        this.platforms = platforms;
        this.speed = speed;

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
        int index = 0;
        while (true)
        {
            if (index >= timbers.Count || platforms.Count == 0)
            {
                break; 
            }

            if (timbers[index].transform.position.x < platforms.First().transform.position.x - 10 ||
                timbers[index].transform.position.x > platforms.Last().transform.position.x)
            {
                Destroy(timbers[index]);
                timbers.RemoveAt(index);
            }
            else
            {
                index += 1; 
            }
        }
        
        FillRivers();
    }

    private void FillRivers()
    {
        Vector3 left = platforms.First().transform.position + Vector3.left * 10;
        Vector3 right = platforms.Last().transform.position;
        Vector3 position; 

        for (float i = left.x; i < right.x; i+=Random.Range(5f, 10f))
        {
            position = new Vector3(i, 0, 0);
            
            if (CheckPositionAbilityToSpawnTimber(position + transform.position))
            {
                CreateTimber(position);
            }
        }
    }

    private bool CheckPositionAbilityToSpawnTimber(Vector3 position)
    {
        RaycastHit rightHit, leftHit;

        if (!Physics.Raycast(position, Vector3.right, out rightHit, 5f) && 
            !Physics.Raycast(position, Vector3.left, out leftHit, 5f))
        {
            return true; 
        }

        return false;
    }

    private void CreateTimber(Vector3 position)
    {
        timbers.Add(Instantiate(timberPrefabs[Random.Range(0, timberPrefabs.Count)], position, Quaternion.identity));
        timbers.Last().GetComponent<TimberController>().speed = speed; 
        timbers.Last().transform.SetParent(transform, false);
    }
}
