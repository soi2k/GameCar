using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SilentButton : BaseButton
{
    [SerializeField] protected TextMeshProUGUI tmp;
    protected bool isActive = true;
    protected override void OnClick()
    {
        tmp = GetComponentInChildren<TextMeshProUGUI>();
        if (isActive)
        {
            SoundManager.Instance._audiosource.enabled = false;
            tmp.text = "Bật Âm";
            isActive = false;
        }
        else
        {
            SoundManager.Instance._audiosource.enabled = true;
            tmp.text = "Tắt Âm";
            isActive = true;
        }    

    }
}
       
