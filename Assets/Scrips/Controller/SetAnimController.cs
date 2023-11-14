using System;
using UnityEngine;
using Spine.Unity;

public class SetAnimController : MonoBehaviour,ISetAnim
{
    public void SetAnim(string name, GameObject gameObject, bool blLoop)
    {
        SkeletonAnimation skeletonAnimation = gameObject.GetComponent<SkeletonAnimation>();
        skeletonAnimation.AnimationState.SetAnimation(0, name, blLoop);
    }
}