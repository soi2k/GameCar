using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideRoadLoopController : LoopMap,IObserver
{
    Subject subject;
    protected override void Awake()
    { 
        base.Awake();
        subject = FindObjectOfType<PlayerManager>();
        subject.AddObserver(this);
    }

    public void OnNotify(float value)
    {
        speed = 0.6f * value;
    }
}
