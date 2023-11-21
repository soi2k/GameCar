using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Car : MonoBehaviour
{
    protected float moveTime;
    protected Vector3 startPst;
    protected Vector3 targetPst;

    public abstract void Move(float moveTime, Vector3 startPst, Vector3 targetPst);
    public abstract void SetState(string typeCar, int animNumber, GameObject gameObject);
}
