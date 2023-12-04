using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{   
    public static GameManager Instance { get; private set; }
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

    public void ReStart()
    {
        Instance.ResetData();
        SceneManager.LoadScene(0);
    }
    public void ResetData()
    {
        lstCarAuto.Clear();
        lstCarAuto.AddRange(new List<string> { "PinkCar", "BlueCar", "OrangeCar" });
        IDCar = ""; 
    }
}