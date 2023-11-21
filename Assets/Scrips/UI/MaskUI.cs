using UnityEngine;
using UnityEngine.UI;

public class MaskUI : MonoBehaviour, IObserver
{
    private Image mask;
    private Subject subject;
    private void Awake()
    {
        mask = GetComponent<Image>();
        subject = FindObjectOfType<MapManager>();
        subject.AddObserver(this);
    }
    public void OnNotify(float value)
    {
        mask.enabled = true;
    }
}
