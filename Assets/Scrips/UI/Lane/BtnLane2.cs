using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnLane2 : BaseButton
{
    private Vector3 Lane2 = new Vector3(-8, -0.4f, 0);

    protected override void OnClick()
    {
        PlayerManager.Instance.ChangeLane(Lane2);
        MapManager.Instance.ChooseRightLane(Lane2.y);
    }
}