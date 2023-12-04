using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CarParttern : MonoBehaviour
{
    public abstract void Move(float moveTime, Vector3 startPst, Vector3 targetPst);
    public abstract void SetState(string typeCar, int animNumber, GameObject gameObject);
}
