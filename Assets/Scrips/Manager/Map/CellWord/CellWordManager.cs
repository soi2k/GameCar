using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class CellWordManager : Singleton<CellWordManager>,IObserver
{
    Subject subject;
    ISetActiveCell setActiveCell;
    IMoveToTarget moveToTarget;
    IFadeinAlphabet fadeinAlphabet;

    private void Start()
    {
        subject = FindObjectOfType<PlayerManager>();
        setActiveCell = GetComponent<ISetActiveCell>();
        moveToTarget = GetComponent<IMoveToTarget>();
        fadeinAlphabet = GetComponent<IFadeinAlphabet>();
        subject.AddObserver(this);
    }
    public void OnNotify(float value)
    {
        setActiveCell.SetActiveCell();
    }

    public void FadeinCellWord()
    {
        moveToTarget.MoveToTarget(0.5f, this.transform.position, new Vector3(0, 0, 0));
        this.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        fadeinAlphabet.FadeinAlphabet();
    }
}

