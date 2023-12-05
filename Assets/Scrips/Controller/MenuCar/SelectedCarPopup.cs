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
        GameObject car = MenuManager.Instance.selectedCar;
        startPst = car.transform.position;
        targetPst = carMid.transform.position;
       
        if (car != carMid)
        {
            carMid.SetActive(false);
            if (car == MenuManager.Instance.lstCar[0])
            {
                MenuManager.Instance.lstCar[2].SetActive(false);
            }
            else
            {
                MenuManager.Instance.lstCar[0].SetActive(false);
            }
            car.GetComponent<IMoveToTarget>().MoveToTarget(duration, startPst, targetPst);
        }
        else
        {
            MenuManager.Instance.lstCar[0].SetActive(false);
            MenuManager.Instance.lstCar[2].SetActive(false);
        }
    }
}
