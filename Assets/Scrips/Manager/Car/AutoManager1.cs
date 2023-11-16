using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoManager1 : Singleton<AutoManager1>
{
    [SerializeField] private GameObject autocarparttern;
    Car car;

    public GameObject autoCar { get; private set; }
    private Vector3 startPst;
    private Vector3 targetPst;
    private Vector3 forWardPst;
    private Vector3 backWardPst;
    private float moveTime = 1.75f; 


    private string typeCar;

    private void Start()
    {
        car = GetComponent<AutoCar>();
        startPst = transform.position;
        targetPst = new Vector3(19, transform.position.y, transform.position.z);

        typeCar = StorageTypeCarManager.Instance.lstCarAuto[0];
        InitPlayCar();
    }
    private void InitPlayCar()
    {
        autoCar = Instantiate(autocarparttern, transform.position, Quaternion.identity);
        autoCar.transform.SetParent(this.transform);
        car.SetState(typeCar, 0, autoCar);
    }
    public void StartPlay()
    {
        car.SetState(typeCar, 1, autoCar);
        car.Move(moveTime, startPst, targetPst);
    }
    public void ChangeLane(float newPositionY)
    {
        car.Move(0.2f, transform.position, new Vector3(transform.position.x, newPositionY, transform.position.z));
    }

    public void MoveForWard()
    {
        forWardPst = new Vector3(19, transform.position.y, transform.position.z);
        car.Move(3, transform.position, forWardPst);
    }
    public void MoveBackWard()
    {
        backWardPst = new Vector3(-19, transform.position.y, transform.position.z);
        car.Move(3, transform.position, backWardPst);
    }
}
