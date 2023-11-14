using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefereeManager : Singleton<RefereeManager>
{
    public GameObject referee;
    ILoadReferee loadReferee;
    ISetAnim setAnim;

    private Vector3 loadPositionStartGame = new Vector3(-4.08f,1.93f,0);
    private Vector3 loadPositionEndingGame = new Vector3(-4.08f,1.93f,0); // Consider
    private void Awake()
    {
        loadReferee = GetComponent<ILoadReferee>();
        setAnim = GetComponent<ISetAnim>();
        loadReferee.LoadReferee(loadPositionStartGame);
    }
    public void StartGo()
    {
        setAnim.SetAnim(ConvertStringController.Convert(AnimReferee.Go), referee, false);
    }

    public void LoadRefereeEnding() // unused
    {
        loadReferee.LoadReferee(loadPositionEndingGame);
    }
}
