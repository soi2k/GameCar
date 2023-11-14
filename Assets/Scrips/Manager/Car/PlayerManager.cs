using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Subject
{
    public static PlayerManager Instance { get; private set; }
    [SerializeField] private GameObject carparttern;
    public GameObject playcar { get; private set; }

    Car car;
    IStartCownDown startCownDown;

    string typeCar;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        car = GetComponent<PlayCar>();
        startCownDown = GetComponent<IStartCownDown>();
        typeCar = StorageTypeCarManager.Instance.IDCar;
        InitPlayCar();
        StartCoroutine(StartGame());
        
    }
    private void InitPlayCar()
    {
        playcar = Instantiate(carparttern,transform.position, Quaternion.identity);
        playcar.transform.SetParent(this.transform);;
        car.SetState(typeCar, 0, playcar);
    }

    private IEnumerator StartGame()
    {   
        SoundManager.Instance.PlaySound(SoundType.StadiumCrowdCheering);
        yield return new WaitForSeconds(1);
        SoundManager.Instance.PlaySound(SoundType.Intro);
        yield return new WaitForSeconds(5);
        SoundManager.Instance.StopSound();
        startCownDown.StartCowntDown();
        SoundManager.Instance.PlaySound(SoundType.CountDown);
        yield return new WaitForSeconds(4);
        SoundManager.Instance.StopSound();
        MusicManager.Instance.PlayMusic(MusicType.PlayGame);

        RefereeManager.Instance.StartGo();
        yield return new WaitForSeconds(0.25f);
        AutoManager1.Instance.StartPlay();
        yield return new WaitForSeconds(0.25f);
        AutoManager2.Instance.StartPlay();
        NotifyNormal();
        car.SetState(typeCar, 1, playcar);
    }
    
    public void ChangeLane(Vector3 newPosition)
    {
        float Auto1PositionY = AutoManager1.Instance.transform.position.y;
        float Auto2PositionY = AutoManager2.Instance.transform.position.y;
        float moveTime;
        float positionY = this.transform.position.y;

        if (Mathf.Abs(positionY - newPosition.y) == 4)
        {
            moveTime = 0.35f;
            car.Move(moveTime, transform.position, newPosition);
        }
        else if (Mathf.Abs(positionY - newPosition.y) == 2)
        {
            moveTime = 0.25f;
            car.Move(moveTime, transform.position, newPosition);
        }
        else return;
        if(newPosition.y == Auto1PositionY)
        {
            AutoManager1.Instance.ChangeLane(positionY);
        }
        else if(newPosition.y == Auto2PositionY)
        {
            AutoManager2.Instance.ChangeLane(positionY);
        }
    }
}
