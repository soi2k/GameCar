using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDialogButton : BaseButton
{
    protected override void OnClick()
    {
        GameObject parentObject = transform.parent.gameObject;
        GameObject Item = GameObject.Find("Items");

        if (parentObject != null) parentObject.SetActive(false);
        foreach (Transform childItem in Item.transform)
        {
            Collider2D childColider = childItem.GetComponent<Collider2D>();
            childColider.enabled = true;
        }
    }
}
