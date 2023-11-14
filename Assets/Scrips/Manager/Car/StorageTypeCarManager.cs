using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StorageTypeCarManager : MonoBehaviour
{   
    public static StorageTypeCarManager Instance { get; private set; }
    public List<string> lstCarAuto = new List<string>
    {
        "PinkCar", "BlueCar", "OrangeCar"
    };
    public string IDCar;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GetListCar()
    {
        for (int i = 0; i < lstCarAuto.Count; i++)
        {
            if (IDCar == lstCarAuto[i])
            {
                lstCarAuto.RemoveAt(i);
            }
        }
    }
}