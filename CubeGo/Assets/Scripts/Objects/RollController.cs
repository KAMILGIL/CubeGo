using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollController : MonoBehaviour
{
    private Vector3 start, end;
    private PlayerController playerController;

    private List<PlatformController> platforms; 
    
    private List<JunkController> junk = new List<JunkController>();
    private GameObject junkPrefab;

    public void SetRoll(Vector3 start, Vector3 end, PlayerController playerController, List<PlatformController> platforms)
    {
        this.start = start;
        this.end = end; 
        this.playerController = playerController;
        this.platforms = platforms;
        
        if (transform.localPosition.y > 0)
        {
            transform.localRotation = Quaternion.Euler(new Vector3(-90f, 0f, 0f));
        }
    }

    private void Update()
    {
        
    }

    private void CheckJunk()
    {
        
    }
    
}


public class Roll
{
    public Vector3 start, end;

    public Roll(Vector3 start, Vector3 end)
    {
        this.start = end;
        this.end = end; 
    }
}