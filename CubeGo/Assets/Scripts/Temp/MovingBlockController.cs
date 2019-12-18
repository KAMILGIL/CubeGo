using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlockController : MonoBehaviour
{
    private Vector3 speed = Vector3.zero;

    private GameObject player;

    public void StartMoving()
    {
        speed = Vector3.forward * 2.5f * Time.deltaTime;
    }

    private void Update()
    {
        transform.position += speed;
        if (speed != Vector3.zero)
        {
            player.transform.localPosition = Vector3.up;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "BottomCollider")
        {
            player = other.transform.parent.gameObject;
            player.transform.SetParent(transform, false);
            StartMoving();
            player.transform.localPosition = Vector3.up;
        }
    }
}
