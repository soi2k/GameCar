using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class InputNameManager : Singleton<InputNameManager>
{
    private ObjectPlayer objectPlayer = new ObjectPlayer();
    private PlayerData playerData = new PlayerData();
    private JsonAPICall jsonAPICall = new JsonAPICall();

    public void ReceiveName(string playerName)
    {
        objectPlayer.name = playerName;
        GetDataFromSever();
    }

    public void GetDataFromSever()
    {
        StartCoroutine(jsonAPICall.JsonGet("/1", "", OnRequesSuccess));
    }

    public void OnRequesSuccess(UnityWebRequest unityWebRequest, string jsonStringResponse)
    {
        UnityWebRequest.Result re = unityWebRequest.result;
        Debug.Log("Result: " + re.ToString());

        playerData = playerData.FromJSON(jsonStringResponse);
        playerData.lstData.Insert(0, objectPlayer);
        PutDataToSever();
    }

    public void PutDataToSever()         
    {
        if(playerData.lstData.Count > 10)
        {
            playerData.lstData.RemoveAt(10);
        }
        string dataPush = JsonUtility.ToJson(playerData);
        StartCoroutine(jsonAPICall.JsonPut("/1", dataPush));
    }
}
