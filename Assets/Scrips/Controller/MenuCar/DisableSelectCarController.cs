using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisableSelectCarController : MonoBehaviour,IDisableButton
{     
    public void DisableButton()
    {
        foreach (Transform childtrans in gameObject.transform)
        {
            Button button = childtrans.GetComponent<Button>();
            if (button != null)
                button.interactable = false;
        }    
    }
}
