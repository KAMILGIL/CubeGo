using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    public GameObject platform;

    private void Start()
    {
        platform = transform.parent.parent.parent.gameObject;
    }
}
