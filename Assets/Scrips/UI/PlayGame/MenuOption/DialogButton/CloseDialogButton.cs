using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDialogButton : BaseButton
{
    protected override void OnClick()
    {
        GameObject parentObject = transform.parent.gameObject;
        if (parentObject != null) parentObject.SetActive(false);

        Transform parent = this.transform.parent.parent;
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);
            if (child.CompareTag("ListParent"))
            {
                child.gameObject.SetActive(true);
            }
        }
    }
}
