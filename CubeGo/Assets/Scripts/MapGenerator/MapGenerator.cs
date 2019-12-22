using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class MapGenerator : MonoBehaviour
{
    public GameObject platformPrefab;

    public Dictionary<GameObject, GameObject[]> rows = new Dictionary<GameObject, GameObject[]>();

    private Vector3 wallDelta = Vector3.forward * 4 + Vector3.up * 4;

    private Vector3 floor = Vector3.zero, wallUp = Vector3.right * 90f; 

    private void Start()
    {
        Instantiate(platformPrefab, Vector3.zero, Quaternion.identity);
        Instantiate(platformPrefab, wallDelta, Quaternion.Euler(Vector3.right * 90f));
    }

    public void MovedRight(GameObject platform)
    {
    }

    public void MovedLeft(GameObject platform)
    {
    } 

    public void MovedForward(GameObject platform)
    {
        
    }

    public void MovedBackward(GameObject platform)
    {
        
    }

    private void CheckPlatform(GameObject platform, int depth)
    {
        var row = rows[platform];

        if (platform.transform.localEulerAngles == floor)
        {
            for (int i = 0; i < depth; i++)
            {
                
            }
        }
    }

    private bool CompareVectors(Vector3 vector1, Vector3 vector2, float epsilon)
    {
        if (Math.Abs(vector1.x - vector2.x) <= epsilon && Math.Abs(vector1.y - vector2.y) <= epsilon && Math.Abs(vector1.z - vector2.z) <= epsilon)
        {
            return true;
        }

        return false;
    }
}

/* 
public class MapGenerator : MonoBehaviour
{
    public GameObject platformPrefab;

    private Vector3 wallDelta = Vector3.forward * 4 + Vector3.up * 4;

    private int platformWidth = 25, platformHeight = 9, generationDepth = 2;

    private Dictionary<Vector3, GameObject> platforms = new Dictionary<Vector3, GameObject>();

    private Vector3 floor = Vector3.zero, wallUp = Vector3.right * 90f;

    private void Start()
    {
        platforms.Add(Vector3.zero, Instantiate(platformPrefab, Vector3.zero, Quaternion.identity));
        platforms.Add(wallDelta, Instantiate(platformPrefab, wallDelta, Quaternion.Euler(Vector3.right * 90f)));
    }

    public void MovedRight(GameObject platform)
    {
        var deltaVector = Vector3.right * platformWidth;
        Vector3 rightBackPosition, rightPosition, rightUpPosition;
        
        if (platform.transform.rotation.eulerAngles == floor)
        {
            for (int i = 0; i < generationDepth; i++)
            {
                rightPosition = platform.transform.position + deltaVector * i;
                rightUpPosition = rightPosition + wallDelta;
                rightBackPosition = rightPosition - wallDelta;
                
                if (!PlatformExists(rightPosition))
                {
                    CreatePlatform(rightPosition, floor);
                }

                if (!PlatformExists(rightUpPosition))
                {
                    CreatePlatform(rightUpPosition, wallUp);
                }

                if (!PlatformExists(rightBackPosition) && platform.transform.position.y != 0)
                {
                    CreatePlatform(rightBackPosition, wallUp);
                }

                if (!PlatformExists(rightUpPosition + wallDelta))
                {
                    CreatePlatform(rightUpPosition + wallDelta, floor);
                }
            }
        }
        else if (platform.transform.rotation.eulerAngles == wallUp)
        {
            for (int i = 0; i < generationDepth; i++)
            {
                rightPosition = platform.transform.position + deltaVector * i;
                rightUpPosition = rightPosition + wallDelta;
                rightBackPosition = rightPosition - wallDelta;
                
                if (!PlatformExists(rightPosition))
                {
                    CreatePlatform(rightPosition, wallUp);
                }

                if (!PlatformExists(rightUpPosition))
                {
                    CreatePlatform(rightUpPosition, floor);
                }

                if (!PlatformExists(rightBackPosition) && platform.transform.position.y != 0)
                {
                    CreatePlatform(rightBackPosition, floor);
                }

                if (!PlatformExists(rightUpPosition + wallDelta))
                {
                    CreatePlatform(rightUpPosition + wallDelta, wallUp);
                }
            }
        }
    }

    public void MovedLeft(GameObject platform)
    {
        var deltaVector = Vector3.left * platformWidth;
        Vector3 leftBackPosition, leftPosition, leftUpPosition; 

        if (platform.transform.rotation.eulerAngles == floor)
        {
            for (int i = 0; i < generationDepth; i++)
            {
                leftPosition = platform.transform.position + deltaVector * i;
                leftUpPosition = leftPosition + wallDelta;
                leftBackPosition = leftPosition - wallDelta;
                
                if (!PlatformExists(leftPosition))
                {
                    CreatePlatform(leftPosition, floor);
                }

                if (!PlatformExists(leftUpPosition))
                {
                    CreatePlatform(leftUpPosition, wallUp);
                }

                if (!PlatformExists(leftBackPosition) && platform.transform.position.y != 0)
                {
                    CreatePlatform(leftBackPosition, wallUp);
                }
            }
        }
    } 

    public void MovedForward(GameObject platform)
    {
        
    }

    public void MovedBackward(GameObject platform)
    {
        
    }

    private void CheckPlatformRow(Vector3 mainPosition, int depth)
    {
        
    }

    private void CreatePlatform(Vector3 position, Vector3 rotation)
    {
        print("fuck");
        platforms.Add(position, Instantiate(platformPrefab, position, Quaternion.Euler(rotation)));
        var controller = platforms[position].GetComponent<PlatformController>();;
    }

    private bool PlatformExists(Vector3 position)
    {
        if (platforms.ContainsKey(position))
        {
            return true; 
        }

        return false;
    }
    
    private bool CompareVectors(Vector3 vector1, Vector3 vector2, float epsilon)
    {
        if (Math.Abs(vector1.x - vector2.x) <= epsilon && Math.Abs(vector1.y - vector2.y) <= epsilon && Math.Abs(vector1.z - vector2.z) <= epsilon)
        {
            return true;
        }

        return false;
    }
}
*/ 