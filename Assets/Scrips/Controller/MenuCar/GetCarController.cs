using System.Collections.Generic;
using UnityEngine;

public class GetCarController : MonoBehaviour,IGetIDCar,IGetObjectCar
{
    [SerializeField] public List<IDCar> lstIDCar;

    public string GetIDCar()
    {
        return lstIDCar[0].ToString();
    } 
    public GameObject GetGameObject()
    {
        return transform.parent.gameObject;
    }
    
}
