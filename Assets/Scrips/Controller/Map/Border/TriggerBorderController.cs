using UnityEngine;

public class TriggerBorderController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Addforce") && !collision.CompareTag("Line"))
        {
            StartCoroutine(MapManager.Instance.WordMiss());
        }
        else
        {
            Destroy(collision.gameObject);
        }
    }
}