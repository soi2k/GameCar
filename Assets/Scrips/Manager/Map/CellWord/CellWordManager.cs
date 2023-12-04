using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class CellWordManager : Singleton<CellWordManager>
{
    ISetActiveCell setActiveCell;
    IMoveToTarget moveToTarget;
    IFadeinAlphabet fadeinAlphabet;

    private void Start()
    {
        setActiveCell = GetComponent<ISetActiveCell>();
        moveToTarget = GetComponent<IMoveToTarget>();
        fadeinAlphabet = GetComponent<IFadeinAlphabet>();
    }
    public void ActiveCellWord()
    {
        setActiveCell.ActiveCell();
    }

    public void FadeinCellWord()
    {
        moveToTarget.MoveToTarget(0.5f, this.transform.position, new Vector3(0.47f, 0, 0));
        this.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        fadeinAlphabet.FadeinAlphabet();
    }
}

