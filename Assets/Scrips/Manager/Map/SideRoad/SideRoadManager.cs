using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideRoadManager : Singleton<SideRoadManager>
{
   [SerializeField] private GameObject preReferee;
    private GameObject referee;

    ISetAnim setAnim;

    private Vector3 loadPositionStartGame = new Vector3(-4.08f,1.93f,0);
    private Vector3 loadPositionEndingGame = new Vector3(-4.08f,1.93f,0); // Consider
    private void Awake()
    {
        
        setAnim = GetComponent<ISetAnim>();
        StartGame();
    }
    public void StartGame()
    {
        referee = Instantiate(preReferee, loadPositionStartGame, Quaternion.identity);
        referee.transform.SetParent(this.transform);
    }
    public void StartGo()
    {
        setAnim.SetAnim(ConvertStringController.Convert(AnimReferee.Go), referee, false);
    }
}
