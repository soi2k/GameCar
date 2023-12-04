using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class PlayGame : BaseButton
{
    public string playerName;
    private TMP_InputField inputField;
    protected override void OnClick()
    {
        inputField = GameObject.FindGameObjectWithTag("InputField").GetComponent<TMP_InputField>();
        GetName();
    }

    private void GetName() 
    {
        this.playerName = inputField.text;
        Debug.Log(playerName);
        if (playerName == null) return;
        if (playerName.All(char.IsWhiteSpace)) return;
        MenuManager.Instance.StartGame();
        InputNameManager.Instance.ReceiveName(playerName);
        transform.parent.gameObject.SetActive(false);
    }
}
