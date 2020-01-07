using UnityEngine;
using Vector3 = UnityEngine.Vector3;

// Player
//   SkinCenter
//     moonPlayer(or whatever)

public class SkinAnimationController : MonoBehaviour
{
    private GameObject skinPivot, skin; 
    
    private Animator animator;

    private Animation jumpingAnimation, pivotRotationAnimation;

    private Vector3 initPosition;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();

        initPosition = transform.localPosition;
        
        skinPivot = transform.GetChild(0).gameObject;
        skin = skinPivot.transform.GetChild(0).gameObject;

        jumpingAnimation = gameObject.AddComponent<Animation>();
        pivotRotationAnimation = skinPivot.AddComponent<Animation>();
    }

    public void StartSkinShrinkingAnimation()
    {
        animator.ResetTrigger("Expand");
        animator.Play("SkinShrinkingAnimation");
    }

    public void StartSkinExpandingAnimation()
    {
        animator.SetTrigger("Expand");
    }
    
    private void SetJumpingAnimationCurve(float topYPosition)
    {
        AnimationCurve curve;
        AnimationClip clip = new AnimationClip();
        clip.name = "jumpAnimation";
        clip.legacy = true;

        Keyframe[] keys;
        keys = new Keyframe[3];
        keys[0] = new Keyframe(0.0f, initPosition.y);
        keys[1] = new Keyframe(SmartSettings.Data.jumpingTime / 2f, topYPosition);
        keys[2] = new Keyframe(SmartSettings.Data.jumpingTime * 1f, initPosition.y);
        curve = new AnimationCurve(keys);
        clip.SetCurve("", typeof(Transform), "localPosition.y", curve);

        jumpingAnimation.AddClip(clip, clip.name);
    }


    private void SetShakingAnimationCurve(float shakeDelta)
    {
        AnimationCurve curve;
        AnimationClip clip = new AnimationClip();
        clip.name = "shakeAnimation";
        clip.legacy = true;

        Keyframe[] keys;
        keys = new Keyframe[3];
        keys[0] = new Keyframe(0.0f, transform.localPosition.y);
        keys[1] = new Keyframe(SmartSettings.Data.shakeTime / 2f, initPosition.y - shakeDelta);
        keys[2] = new Keyframe(SmartSettings.Data.shakeTime * 1f, initPosition.y);
        curve = new AnimationCurve(keys);
        clip.SetCurve("", typeof(Transform), "localPosition.y", curve);

        jumpingAnimation.AddClip(clip, clip.name);
    }
    
    public void PlayJumpingAnimation()
    {
        SetJumpingAnimationCurve(-0.22f);
        jumpingAnimation.Play("jumpAnimation");
    }

    public void PlayShakingAnimation()
    {
        SetShakingAnimationCurve(SmartSettings.Data.shakeDelta);
        jumpingAnimation.Play("shakeAnimation");
    }
    
    private void SetPivotRotationAnimationCurve(float targetRotation)
    {
        if (Mathf.Abs(skinPivot.transform.localEulerAngles.y) <= 0.1f && targetRotation == 270f)
        {
            targetRotation = -90f;
        }
        if (Mathf.Abs(skinPivot.transform.localEulerAngles.y - 270) <= 0.1f && targetRotation == 0f)
        {
            targetRotation = 360f;
        }
        
        AnimationCurve curve;
        AnimationClip clip = new AnimationClip();
        clip.name = "rotationAnimation";
        clip.legacy = true;

        Keyframe[] keys;
        keys = new Keyframe[2];
        keys[0] = new Keyframe(0.0f, pivotRotationAnimation.transform.localEulerAngles.y);
        keys[1] = new Keyframe(SmartSettings.Data.jumpingTime, targetRotation);
        curve = new AnimationCurve(keys);
        clip.SetCurve("", typeof(Transform), "localEulerAngels.y", curve);

        pivotRotationAnimation.AddClip(clip, clip.name);
    }

    private void PlayPivotRotationAnimation()
    {
        pivotRotationAnimation.Play("rotationAnimation");
    }
    
    public void LookForward()
    {
        SetPivotRotationAnimationCurve(180);
        PlayPivotRotationAnimation();
    }

    public void LookLeft()
    {
        SetPivotRotationAnimationCurve(90);
        PlayPivotRotationAnimation();
    }

    public void LookRight()
    {
        SetPivotRotationAnimationCurve(270);
        PlayPivotRotationAnimation();
    }

    public void LookBack()
    {
        SetPivotRotationAnimationCurve(0);
        PlayPivotRotationAnimation();
    }
}