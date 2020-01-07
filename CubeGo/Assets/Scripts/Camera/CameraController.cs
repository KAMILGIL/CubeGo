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
        
        camera = GetComponent<Camera>(); 
        
        if (!SmartSettings.Data.isPlainMode)
        {
            transform.position = new Vector3(5, 20, -20);
        }
        
        playerController = player.GetComponent<PlayerController>();
        playerController.cameraController = this;
        transform.LookAt(player.transform);

        playerDelta = transform.position - player.transform.position;

        movingAnimation = gameObject.AddComponent<Animation>();

        mapGenerator = gameObject.GetComponent<MapGenerator>();

        playerController.mapGenerator = mapGenerator;
        mapGenerator.playerController = playerController;
        mapGenerator.InitMap();
    }

    private void Update()
    {
        if (!movingAnimation.isPlaying)
        {
            transform.position += speed * Time.deltaTime;
            transform.position += playerController.speed * Time.deltaTime;
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

        transform.position += (playerController.transform.position + playerDelta - transform.position) * Time.deltaTime * 2.8f;
    }

    public void MoveCamera(Vector3 position)
    {
        //target = position + playerDelta;
        //SetMovingAnimation();
        //PlayMovingAnimation();
    }
    
    private AnimationCurve SetMovingAnimationCurve(float initValue, float targetValue)
    {
        AnimationCurve curve;

        Keyframe[] keys;
        keys = new Keyframe[2];
        keys[0] = new Keyframe(0.0f, initValue);
        keys[1] = new Keyframe(SmartSettings.Data.jumpingTime * 2.5f, targetValue);
        curve = new AnimationCurve(keys);

        return curve;
    }

    private void SetMovingAnimation()
    {
        AnimationClip clip = new AnimationClip();
        clip.name = "movingAnimation";
        clip.legacy = true;
        clip.SetCurve("", typeof(Transform), "localPosition.x", SetMovingAnimationCurve(transform.localPosition.x, target.x));
        if (playerController.transform.position.z >= (transform.localPosition- playerDelta).z || true)
        {
            clip.SetCurve("", typeof(Transform), "localPosition.z", SetMovingAnimationCurve(transform.localPosition.z, target.z));
        }
        else
        {
            clip.SetCurve("", typeof(Transform), "localPosition.z", SetMovingAnimationCurve(transform.localPosition.z, transform.localPosition.z + speed.z * SmartSettings.Data.jumpingTime * 2.5f));
        }
        clip.SetCurve("", typeof(Transform), "localPosition.y", SetMovingAnimationCurve(transform.localPosition.y, target.y));

        movingAnimation.AddClip(clip, clip.name);
    }

    private void PlayMovingAnimation()
    {
        movingAnimation.Play("movingAnimation");
    }
}
