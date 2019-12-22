using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public int width, length;

    private Dictionary<Vector3, GameObject> blocks = new Dictionary<Vector3, GameObject>();

    public void SetPlatformCenter()
    {
    }

    private void GetBlocks()
    {
    }
}
