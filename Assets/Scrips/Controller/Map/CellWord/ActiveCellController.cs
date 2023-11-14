using UnityEngine;


public class ActiveCellControllerLoad : MonoBehaviour, ISetActiveCell
{
    SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void SetActiveCell()
    {
        spriteRenderer.enabled = true;
    }
}
