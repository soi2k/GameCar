using UnityEngine;


public class ActiveCellController : MonoBehaviour, ISetActiveCell
{
    SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void ActiveCell()
    {
        spriteRenderer.enabled = true;
    }
}

