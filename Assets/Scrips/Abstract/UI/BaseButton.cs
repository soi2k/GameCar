using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public abstract class BaseButton : MonoBehaviour
{
    [Header("Button")]
    [SerializeField] protected Button button;

    protected void Awake()
    {
        if (button != null) return;
        this.button = GetComponent<Button>();
    }
    protected void Start()
    {
        this.AddOnClickEvent();
    }
    protected virtual void AddOnClickEvent()
    {
        button.onClick.AddListener(this.OnClick);
    }

    protected abstract void OnClick();
}
