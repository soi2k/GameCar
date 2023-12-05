using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateRankController : MonoBehaviour,IUpdateName
{
    private GameObject objListName;
    private void Awake()
    {
        objListName = GameObject.FindGameObjectWithTag("ListName");
    }

    public void UpdateName(PlayerData playerData)
    {
        for(int i = 0; i < objListName.transform.childCount - 1; i++)
        {
            Transform child = objListName.transform.GetChild(i + 1);
            child.GetComponent<TextMeshProUGUI>().text = playerData.lstData[i].name;
        }
    }
    
}
