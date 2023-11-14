using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LoopMap : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] protected Transform bg1;
    [SerializeField] protected Transform bg2;
    float m_xsize;
    protected bool isStart = true;

    protected virtual void Awake()
    {
        m_xsize = bg1.GetComponent<SpriteRenderer>().bounds.size.x;
        bg1.transform.position = new Vector3(0, 0, 0);
        bg2.transform.position = new Vector3(
            bg1.transform.position.x + m_xsize,
            bg1.transform.position.y,
            0f);
    }
    protected void Update()
    {
        if (!isStart) return;
        transform.Translate(Vector3.left * speed * Time.deltaTime);
        if (bg1.position.x <= -m_xsize)
        {
            bg1.position = new Vector3(
                bg2.transform.position.x + m_xsize,
                bg2.transform.position.y,
                0f);
        }

        Transform temp = bg1;
        bg1 = bg2;
        bg2 = temp;
    }
}
