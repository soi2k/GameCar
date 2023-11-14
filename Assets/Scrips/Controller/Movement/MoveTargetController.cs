using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTargetController : MonoBehaviour, IMoveToTarget
{
    private float timeElapse;
    public virtual void MoveToTarget(float moveTime, Vector3 startPst, Vector3 targetPst)
    {
        StartCoroutine(MoveIEnumerator(moveTime, startPst, targetPst));
    }

    protected IEnumerator MoveIEnumerator(float moveTime, Vector3 startPst, Vector3 targetPst)
    {
        timeElapse = 0;
        while (timeElapse < moveTime)
        {
            timeElapse += Time.deltaTime;
            float t = Mathf.Clamp01(timeElapse / moveTime);
            Vector3 newPosition = Vector3.Lerp(startPst, targetPst, t);
            transform.position = newPosition;
            yield return null;
        }
        transform.position = targetPst;
        timeElapse = 0;
    }
}
