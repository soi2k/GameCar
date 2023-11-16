using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnLane1 : BaseButton
{
    private Vector3 Lane1 = new Vector3(-9, -2.4f, 0);
    protected override void OnClick()
    {
        PlayerManager.Instance.ChangeLane(Lane1);
        MapManager.Instance.ChooseRightLane(Lane1.y);
    }
}
