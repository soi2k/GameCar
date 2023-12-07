using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLaneController : MonoBehaviour,IChangeLane
{
    CarParttern car;
    private void Start()
    {
        car = GetComponent<Car>();
    }
    public void ChangeLane(Vector3 newPosition)
    {
        if (!PlayerManager.Instance.IsChangeLane()) return;
        float Auto1PositionY = AutoManager1.Instance.transform.position.y;
        float Auto2PositionY = AutoManager2.Instance.transform.position.y;
        float moveTime;
        float positionY = this.transform.position.y;

        if (Mathf.Abs(positionY - newPosition.y) == 4)
        {
            moveTime = 0.35f;
            car.Move(moveTime, transform.position, newPosition);
            PlayerManager.Instance.startPosition = newPosition;
        }
        else if (Mathf.Abs(positionY - newPosition.y) == 2)
        {
            StartCoroutine(PlayerManager.Instance.FirstGuiding());
            moveTime = 0.25f;
            car.Move(moveTime, transform.position, newPosition);
            PlayerManager.Instance.startPosition = newPosition;
        }
        else return;
        if (newPosition.y == Auto1PositionY)
        {
            AutoManager1.Instance.ChangeLane(positionY);
        }
        else if (newPosition.y == Auto2PositionY)
        {
            AutoManager2.Instance.ChangeLane(positionY);
        }
    }
}
