using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SAnimator : MonoBehaviour
{
    public Animation animation;
    private AnimationCurve animationCurve;
    
    public void AnimatePosition(Vector3[] positions)
    {
        
    }

    public AnimationCurve GetAnimationCurve(float[] values)
    {
        return new AnimationCurve();
    }
}

public class SAnimationState
{
    public SAnimationState(float time, Vector3 position)
    {
        
    }
}