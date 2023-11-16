using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoutainLoop : LoopMap,IObserver
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
        speed = 3;
    }

    public void OnNotifyTrigger()
    {
        speed = 6;
    }

    public void OnNotifyAddForce()
    {
        speed = 10.5f;
    }
}
