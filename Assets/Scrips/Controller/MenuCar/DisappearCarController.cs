using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearCarController : MonoBehaviour
{
    IMoveToTarget moveToTarget;
    float moveTime = 0.35f;
    Vector3 startPst;
    Vector3 targetPst;
    private void Start()
    {
        moveToTarget = GetComponent<IMoveToTarget>();
        startPst = transform.position;
        targetPst = new Vector3(transform.position.x, Constant.positionYDisappear, transform.position.z);
        moveToTarget.MoveToTarget(moveTime, startPst, targetPst);
    }
}
