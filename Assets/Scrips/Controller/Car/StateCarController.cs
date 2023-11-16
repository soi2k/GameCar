using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class StateCarController : MonoBehaviour,ISetState
{
    ISetAnim setAnim;
    Enum[] lstPinkCars = { PinkCar.Start, PinkCar.Move, PinkCar.Fadein, PinkCar.Flicker, PinkCar.AddForce1, PinkCar.AddForce2 };
    Enum[] lstBlueCars = { BlueCar.Start, BlueCar.Move, BlueCar.Fadein, BlueCar.Flicker,BlueCar.AddForce1, BlueCar.AddForce2 };
    Enum[] lstOrangeCars = { OrangeCar.Start, OrangeCar.Move, OrangeCar.Fadein, OrangeCar.Flicker, OrangeCar.AddForce1, OrangeCar.AddForce2 };
    public void SetState(string typeCar, int animNumber, GameObject gameObject)
    {
        setAnim = GetComponent<ISetAnim>();
        switch (typeCar)
        {
            case "PinkCar":
                setAnim.SetAnim(ConvertStringController.Convert(lstPinkCars[animNumber]),gameObject, true);
                break;
            case "BlueCar":
                setAnim.SetAnim(ConvertStringController.Convert(lstBlueCars[animNumber]), gameObject, true);
                break;
            case "OrangeCar":
                setAnim.SetAnim(ConvertStringController.Convert(lstOrangeCars[animNumber]), gameObject, true);
                break;
        }
    }
}