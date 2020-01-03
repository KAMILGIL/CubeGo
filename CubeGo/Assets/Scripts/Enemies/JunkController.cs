using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunkController : MonoBehaviour
{
    public Vector3 speed;

    public GameObject skin;

    public Vector3 length;

    private Animation movingAnimation;

    private void Update()
    {
        if (!movingAnimation.isPlaying)
        {
            StartMovingAnimation();
        }
    }

    public void SetJunk(Vector3 length)
    {
        this.length = length;
    }
    

    public void SetSkin()
    {
        
    }


    private AnimationCurve GetCurve(float initValue, float targetValue)
    {
        AnimationCurve curve;
        
        Keyframe[] keys;
        keys = new Keyframe[2];
        keys[0] = new Keyframe(0.0f, initValue);
        keys[1] = new Keyframe(PlayerSmartSettings.jumpingTime, targetValue);
        curve = new AnimationCurve(keys);

        return curve;
    }

    private void StartMovingAnimation()
    {
        Vector3 target = transform.localPosition + Vector3.right;
        AnimationClip clip = new AnimationClip();
        clip.name = "movingAnimation";
        clip.legacy = true;

        clip.SetCurve("", typeof(Transform), "localPosition.x", GetCurve(transform.localPosition.x, target.x));
        clip.SetCurve("", typeof(Transform), "localPosition.y", GetCurve(transform.localPosition.y, target.y));
        clip.SetCurve("", typeof(Transform), "localPosition.z", GetCurve(transform.localPosition.z, target.z));

        movingAnimation.AddClip(clip, clip.name);

        movingAnimation.Play("movingAnimation");
    }

}
