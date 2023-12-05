using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideRoadManager : Singleton<SideRoadManager>
{
   [SerializeField] private GameObject preReferee;
    private GameObject referee;

    ISetAnim setAnim;

    private Vector3 loadPositionStartGame = new(-4.08f,1.93f,0);
    private void Awake()
    {
        
        setAnim = GetComponent<ISetAnim>();
        StartGame();
    }
    public void StartGame()
    {
        referee = Instantiate(preReferee, loadPositionStartGame, Quaternion.identity);
        referee.transform.SetParent(this.transform);
        referee.AddComponent<BoxCollider2D>();
    }
    public void StartGo()
    {
        setAnim.SetAnim(ConvertStringController.Convert(AnimReferee.Go), referee, false);
    }
}
