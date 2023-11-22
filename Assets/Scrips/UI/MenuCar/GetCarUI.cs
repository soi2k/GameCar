using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCarUI : BaseButton
{
    IGetIDCar getIDCar;
    IGetObjectCar getObjectCar;

    protected override void OnClick()
    {
        getIDCar = GetComponent<IGetIDCar>();
        getObjectCar = GetComponent<IGetObjectCar>();
        GameManager.Instance.IDCar = getIDCar.GetIDCar();
        MenuManager.Instance.selectedCar = getObjectCar.GetGameObject();
        MenuManager.Instance.blSelectedCar = true;
    }
}
