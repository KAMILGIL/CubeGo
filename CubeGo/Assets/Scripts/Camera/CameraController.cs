using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    private MapGenerator mapGenerator;

    private PlayerController playerController;

    private Vector3 playerDelta, target, speed = Vector3.forward * 0.8f;

    private Animation movingAnimation;

    private Camera camera; 

    private void Start()
    {
        if (!PlayerSmartSettings.isPlainMode)
        {
            transform.position = new Vector3(5, 20, -20);
        }
        
        playerController = player.GetComponent<PlayerController>();
        playerController.cameraController = this;
        transform.LookAt(player.transform);

        playerDelta = transform.position - player.transform.position;

        movingAnimation = gameObject.AddComponent<Animation>();

        mapGenerator = gameObject.GetComponent<MapGenerator>();

        mapGenerator.playerController = playerController;
        mapGenerator.InitMap();
        playerController.mapGenerator = mapGenerator;

        camera = GetComponent<Camera>(); 
    }

    private void Update()
    {
        if (!movingAnimation.isPlaying)
        {
            transform.position += speed * Time.deltaTime;
        }


        if (Input.GetKeyDown(KeyCode.R))
        {
            player.transform.position = Vector3.up;
            player.transform.rotation = Quaternion.identity;
            transform.position = player.transform.position + playerDelta;
        }

        if (Input.deviceOrientation == DeviceOrientation.LandscapeLeft ^ Input.deviceOrientation == DeviceOrientation.LandscapeRight)
        {
            camera.orthographicSize = 3.3f;
        }
        else
        {
            camera.orthographicSize = 6.7f;
        }
    }

    public void MoveCamera(Vector3 position)
    {
        target = position + playerDelta;
        SetMovingAnimation();
        PlayMovingAnimation();
    }
    
    private AnimationCurve SetMovingAnimationCurve(float initValue, float targetValue)
    {
        AnimationCurve curve;

        Keyframe[] keys;
        keys = new Keyframe[2];
        keys[0] = new Keyframe(0.0f, initValue);
        keys[1] = new Keyframe(PlayerSmartSettings.jumpingTime * 2.5f, targetValue);
        curve = new AnimationCurve(keys);

        return curve;
    }

    private void SetMovingAnimation()
    {
        AnimationClip clip = new AnimationClip();
        clip.name = "movingAnimation";
        clip.legacy = true;
        clip.SetCurve("", typeof(Transform), "localPosition.x", SetMovingAnimationCurve(transform.localPosition.x, target.x));
        clip.SetCurve("", typeof(Transform), "localPosition.z", SetMovingAnimationCurve(transform.localPosition.z, target.z));
        clip.SetCurve("", typeof(Transform), "localPosition.y", SetMovingAnimationCurve(transform.localPosition.y, target.y));

        movingAnimation.AddClip(clip, clip.name);
    }

    private void PlayMovingAnimation()
    {
        movingAnimation.Play("movingAnimation");
    }
}
