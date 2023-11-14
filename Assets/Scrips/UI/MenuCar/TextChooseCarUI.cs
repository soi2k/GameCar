using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextChooseCarUI : MonoBehaviour,ISetActiveText
{
    TextMeshProUGUI tmp;

    private void Awake()
    {   
        if(tmp == null)
        tmp = GameObject.FindWithTag("TextChooseCar").GetComponent<TextMeshProUGUI>();
    }

    public void EnableText()
    {
        tmp.enabled = true;
    } 
    public void DisableText()
    {
        tmp.enabled = false;
    }

}
