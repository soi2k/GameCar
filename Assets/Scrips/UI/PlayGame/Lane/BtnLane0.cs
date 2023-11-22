using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnLane0 : BaseButton
{
    private Vector3 Lane0 = new Vector3(-10, -4.4f, 0);
    protected override void OnClick()
    {
        PlayerManager.Instance.ChangeLane(Lane0);
        MapManager.Instance.ChooseRightLane(Lane0.y);
    }
}
