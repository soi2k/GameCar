using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCarPopup : MonoBehaviour,IPopupCar
{
    private GameObject carMid;
    private Vector3 startPst;
    private Vector3 targetPst;
    private float duration = 0.7f;

    public void PopupCar()
    {
        carMid = MenuManager.Instance.lstCar[1];
        if(carMid != MenuManager.Instance.selectedCar)
        {   
            GameObject car = MenuManager.Instance.selectedCar;
            startPst = car.transform.position;
            targetPst = carMid.transform.position;
            car.GetComponent<IMoveToTarget>().MoveToTarget(duration, startPst, targetPst);
            carMid.GetComponent<IMoveToTarget>().MoveToTarget(duration, targetPst, startPst);
            
        }
    }
}
