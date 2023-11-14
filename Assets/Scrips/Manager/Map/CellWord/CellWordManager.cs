using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellWordManager : Singleton<CellWordManager>,IObserver
{
    Subject subject;
    ISetActiveCell setActiveCell;

    private void Start()
    {
        subject = FindObjectOfType<PlayerManager>();
        setActiveCell = GetComponent<ISetActiveCell>();
        subject.AddObserver(this);
    }
    public void OnNotify()
    {
        setActiveCell.SetActiveCell();
    }
}
