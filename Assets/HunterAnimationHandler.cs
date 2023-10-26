using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterAnimationHandler : MonoBehaviour
{

    public AnimationReferenceAsset Run, Jump;
    public SkeletonAnimation skeletonAnimation;
    public HunterScript hunterScript;
    public string currentState;
    public string currentAnimation;
   
    void Start()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        hunterScript = GetComponentInParent<HunterScript>();
        currentState = "Run";
        SetCharacterState(currentState);
    }
    
    public void SetAnimation(AnimationReferenceAsset animation, bool loop, float timeScale)
    {
        if (animation.name.Equals(currentAnimation))
            return;
        skeletonAnimation.state.SetAnimation(0, animation, loop).TimeScale = timeScale;
        currentAnimation = animation.name;
    }
    public void SetCharacterState(string state)
    {
        if (state.Equals("Run"))
        {
            if (!hunterScript.upsideDown)
                SetAnimation(Run, true, 1f);
            else
                SetAnimation(Run, true, 2f);
        }
        else if (state.Equals("Jump"))
        {
            SetAnimation(Jump, false, 1f);
        }
    }
}
