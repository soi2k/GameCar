using UnityEngine;

public class LightFadeinController : MonoBehaviour,IStartFadein
{
    private float flashSpeed = 2f; 
    private Color flashColor = new Color(1.0f, 1.0f, 1.0f, 0.5f); 

    private Color originalColor;
    private SpriteRenderer spriteRenderer; 

    private bool isFlashing = false;

    private void Start()
    {   
        if(spriteRenderer == null)
        spriteRenderer = GameObject.FindWithTag("Light").GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.material.color;
    }

    private void Update()
    {
        if (isFlashing)
        {
            float lerp = Mathf.PingPong(Time.time * flashSpeed, 1.0f);
            spriteRenderer.material.color = Color.Lerp(originalColor, flashColor, lerp);
        }
    }

    public void StartFadein()
    {
        spriteRenderer.enabled = true;
        isFlashing = true;
    }

    //public void StopFadein()
    //{
    //    isFlashing = false;
    //    spriteRenderer.material.color = originalColor;
    //}
}
