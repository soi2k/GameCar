using UnityEngine;

public class TriggerBorderController1 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision);
    }
}