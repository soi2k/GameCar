using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadRefereeController : MonoBehaviour,ILoadReferee
{
    [SerializeField] GameObject referee;
    public void LoadReferee(Vector3 loadPositon)
    {
        GameObject go =  Instantiate(referee, loadPositon, Quaternion.identity);
        RefereeManager.Instance.referee = go;
        go.transform.SetParent(this.transform);
    }
}
