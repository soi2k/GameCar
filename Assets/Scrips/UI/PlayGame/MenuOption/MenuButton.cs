using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : BaseButton
{
    Transform parent;
    protected override void OnClick()
    {
        parent = this.transform.parent;
        for(int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);
            if (child.CompareTag("Dialog"))
            {
                child.gameObject.SetActive(true);
            }
            if (child.CompareTag("ListParent"))
            {
                child.gameObject.SetActive(false);
            }
        }
    }
}
