
using System.Collections.Generic;
using UnityEngine;

public abstract class Subject : MonoBehaviour
{
    private List<IObserver> observers = new List<IObserver>();

    public void AddObserver(IObserver observer)
    {
        observers.Add(observer);
    }
    public void RemoveObserver(IObserver observer)
    {
        observers.Remove(observer);
    }
    protected void NotifyNormal()
    {
        observers.ForEach((observer) =>
        {
            observer.OnNotifyNormal();
        });
    }
    protected void NotifyTrigger()
    {
        observers.ForEach((observer) =>
        {
            observer.OnNotifyTrigger();
        });
    }
    protected void NotifyAddforce()
    {
        observers.ForEach((observer) =>
        {
            observer.OnNotifyAddForce();
        });
    }

}