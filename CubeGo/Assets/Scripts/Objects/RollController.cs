using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollController : MonoBehaviour
{
    public Vector3 center, length;
    public PlayerController playerController;

    private List<PlatformController> platforms; 
    
    private List<GameObject> junk = new List<GameObject>();
    private GameObject junkPrefab;

    public void SetRoll(Vector3 center, Vector3 length, PlayerController playerController, List<PlatformController> platforms)
    {
        this.center = center;
        this.length = length;
        this.playerController = playerController;
        this.platforms = platforms;
        
        
        if (transform.localPosition.y > 0)
        {
            transform.localRotation = Quaternion.Euler(new Vector3(-90f, 0f, 0f));
        }
    }
}


public class Roll
{
    public Vector3 center, length;

    public Roll(Vector3 center, Vector3 length)
    {
        this.center = center;
        this.length = length; 
    }
}