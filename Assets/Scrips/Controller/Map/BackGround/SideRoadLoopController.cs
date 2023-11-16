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

    public void OnNotifyNormal()
    {
        speed = 4;
    }
    public void OnNotifyTrigger()
    {
        speed = 8;
    }

    public void OnNotifyAddForce()
    {
        speed = 14;
    }

}
