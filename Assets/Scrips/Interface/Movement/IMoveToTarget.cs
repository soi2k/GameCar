using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveToTarget
{
    public void MoveToTarget(float duration, Vector3 startPst, Vector3 targetPst);
}
