using UnityEngine;
using UnityEngine.UI;

public class MaskUI : Singleton<MaskUI>
{
    private Image mask;
    private void Awake()
    {
        mask = GetComponent<Image>();
        
    }
    public void EnableMask()
    {
        mask.enabled = true;
    }
}
