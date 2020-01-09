using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class TimberController : MonoBehaviour
{
    public int timberLength;

    public Vector3 speed = Vector3.right * 2f; // is speed of the timber

    private GameObject skin, colliderPrefab;

    private List<GameObject> colliders = new List<GameObject>();

    private Animation movingAnimation; 

    private void Start()
    {
        colliderPrefab = Resources.Load<GameObject>("CustomColliders/BlockCollider");

        CreateBlocks("Winter");
    }

    private void Update()
    {
        transform.position += speed * Time.deltaTime;

        foreach (GameObject collider in colliders)
        {
            collider.GetComponent<BlockController>().speed = speed; 
        }

        if (Input.GetKey(KeyCode.T))
        {
            StartMovingAnimation();
        }
    }
    
    private AnimationCurve GetCurve(float initValue, float deltaValue)
    {
        AnimationCurve curve;
        
        Keyframe[] keys;
        keys = new Keyframe[3];
        keys[0] = new Keyframe(0.0f, initValue);
        keys[1] = new Keyframe(SmartSettings.Data.shakeTime / 2f, initValue - deltaValue);
        keys[2] = new Keyframe(SmartSettings.Data.shakeTime, initValue);
        curve = new AnimationCurve(keys);

        return curve;
    }

    public void StartMovingAnimation()
    {
        if (!skin.GetComponent<Animation>())
        {
            movingAnimation = skin.AddComponent<Animation>();
        }

        if (movingAnimation.isPlaying)
        {
            return; 
        }

        AnimationClip clip = new AnimationClip();
        clip.name = "movingAnimation";
        clip.legacy = true;

        clip.SetCurve("", typeof(Transform), "localPosition.y", GetCurve(skin.transform.localPosition.y, SmartSettings.Data.shakeDelta));
        
        movingAnimation.AddClip(clip, clip.name);

        movingAnimation.Play("movingAnimation");
    }

    private void CreateBlocks(string theme)
    {
        string timberType; 
        
        switch (timberLength)
        {
            case 3:
                timberType = "SmallTimber";
                break;
            case 4:
                timberType = "MediumTimber";
                break; 
            case 5:
                timberType = "LargeTimber";
                break;
            default:
                timberType = "none";
                break;
        }
        
        
        skin = Instantiate(Resources.Load<GameObject>("Textures/" + theme + "/EnemySkins/Timbers/" + timberType), Vector3.up * 0.2f, Quaternion.identity);
        skin.transform.SetParent(transform, false);

        for (int i = 0; i < timberLength; i++)
        {
            colliders.Add(Instantiate(colliderPrefab, Vector3.right / 2 + Vector3.right * i + Vector3.left * timberLength / 2 + Vector3.up * 0.2f, Quaternion.identity));
            colliders[i].transform.SetParent(skin.transform, false);
            colliders[i].GetComponent<BlockController>().speed = speed; 
        }
    }
}