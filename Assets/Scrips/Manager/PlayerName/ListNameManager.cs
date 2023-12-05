using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ListNameManager : Singleton<ListNameManager>
{
    private IMoveToTarget moveToTarget;
    private IUpdateName updateName;
    private PlayerData playerData = new PlayerData();
    private JsonAPICall jsonAPICall = new JsonAPICall();
    
    Vector3 targertPst = new Vector3(0, 0, 0);
    // Start is called before the first frame update
    void Start()
    {
        moveToTarget = GetComponent<IMoveToTarget>();
        updateName = GetComponent<IUpdateName>();
        GetDataFromSever();
    }

    public void DisplayDialogName()
    {
        moveToTarget.MoveToTarget(0.2f, transform.position, targertPst);
    }
    public void GetDataFromSever()
    {
        StartCoroutine(jsonAPICall.JsonGet("/1", "", OnRequesSuccess));
    }

    public void OnRequesSuccess(UnityWebRequest unityWebRequest, string jsonStringResponse)
    {
        Debug.Log("GetData Success");
        playerData = playerData.FromJSON(jsonStringResponse);
        updateName.UpdateName(playerData);
    }
}
