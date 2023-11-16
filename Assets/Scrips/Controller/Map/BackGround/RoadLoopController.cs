using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadLoopController : LoopMap,IObserver
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
        speed = 7;
    }
    public void OnNotifyTrigger()
    {
        speed = 14;
    }

    public void OnNotifyAddForce()
    {
        speed = 24.5f;
    }
}
