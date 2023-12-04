using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Subject
{
    public static PlayerManager Instance { get; private set; }
    [SerializeField] private GameObject carparttern;
    [SerializeField] private GameObject prefabguidFinger;
    private GameObject guidFinger;
    public GameObject playcar { get; private set; }

    private CarParttern car;
    private IStartCownDown startCownDown;
    private Vector3 startPosition; 
    private Vector3 guidFingerPst = new Vector3(7, -2, 0); 

    string typeCar;

    private float speedNormalBackGround = 7f;
    private float speedTriggerBackGround = 14f;
    private float speedAddforceBackGround = 24.5f;
    private float stopBackGround = 0f;
    private float soundIntro = 5f;
    private float stateCar4 = 0.3f;

    private bool blChangeLane = false;
    private bool triggerAddforce = false;

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
        car = GetComponent<Car>();
        startCownDown = GetComponent<IStartCownDown>();
        typeCar = GameManager.Instance.IDCar;
        startPosition = transform.position;
        InitPlayCar();
        StartCoroutine(StartGame());
        
    }
    private void InitPlayCar()
    {
        playcar = Instantiate(carparttern, transform.position, Quaternion.identity);
        playcar.transform.SetParent(this.transform);;
        car.SetState(typeCar, 0, playcar);
    }

    private IEnumerator StartGame()
    {
        yield return Constant.wait1s.Wait();
        SoundManager.Instance.PlaySound(SoundType.StadiumCrowdCheering);
        SoundManager.Instance.PlaySound(SoundType.Intro);
        yield return soundIntro.Wait();
        SoundManager.Instance.StopSound();
        SoundManager.Instance.PlaySound(SoundType.CountDown);
        startCownDown.StartCowntDown();
        yield return Constant.wait3s.Wait();
        MusicManager.Instance.PlayMusic(MusicType.PlayGame);

        SideRoadManager.Instance.StartGo();
        yield return Constant.wait1s.Wait();
        AutoManager1.Instance.StartPlay();
        AutoManager2.Instance.StartPlay();
        Notify(speedNormalBackGround);
        CellWordManager.Instance.ActiveCellWord();
        car.SetState(typeCar, 1, playcar);
        yield return Constant.wait2_5s.Wait();
        SoundManager.Instance.PlaySound(SoundType.Guiding);
        guidFinger = Instantiate(prefabguidFinger, guidFingerPst, Quaternion.identity);
        blChangeLane = true;
    }
    
    public void ChangeLane(Vector3 newPosition)
    {
        if (!blChangeLane) return;
        float Auto1PositionY = AutoManager1.Instance.transform.position.y;
        float Auto2PositionY = AutoManager2.Instance.transform.position.y;
        float moveTime;
        float positionY = this.transform.position.y;

        if (Mathf.Abs(positionY - newPosition.y) == 4)
        {                                                                                                                                                                                                                                                                          
            moveTime = 0.35f;
            car.Move(moveTime, transform.position, newPosition);
            startPosition = newPosition;
        }
        else if (Mathf.Abs(positionY - newPosition.y) == 2)
        {
            StartCoroutine(FirstGuiding());
            moveTime = 0.25f;
            car.Move(moveTime, transform.position, newPosition);
            startPosition = newPosition;
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

    private IEnumerator FirstGuiding()
    {
        if (guidFinger != null)
        {
            SoundManager.Instance.StopSound();
            Destroy(guidFinger);
            SoundManager.Instance.PlaySound(SoundType.NiceDriving);
            yield return Constant.wait4s.Wait();
            MapManager.Instance.GenerateAlphabet();
        }
    }

    public void SetActiveChangeLane(bool blChangeLane)
    {   
        this.blChangeLane = blChangeLane;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Addforce")
        {
            StartCoroutine(MapManager.Instance.TriggerAlphabet());
            StartCoroutine(StateTrigger());
        }
        else
        {   if (triggerAddforce) return;
            Destroy(collision.gameObject);
            StartCoroutine(Addforce());
            triggerAddforce = true;
        }
    }
    
    private IEnumerator StateTrigger()
    {
        blChangeLane = false;
        yield return Constant.wait1s.Wait();
        car.SetState(typeCar, 2, playcar);
        yield return Constant.wait1s.Wait();
        car.Move(3, transform.position, new Vector3(0, startPosition.y, startPosition.z));
        Notify(speedTriggerBackGround);
        yield return Constant.wait3s.Wait();
        car.SetState(typeCar, 3, playcar);
        yield return Constant.wait1_5s.Wait();
        car.SetState(typeCar, 1, playcar);
        car.Move(2, transform.position, startPosition);
        yield return Constant.wait2s.Wait();
        Notify(speedNormalBackGround);
        blChangeLane = true;
        yield return Constant.wait1_5s.Wait();
        MapManager.Instance.GenerateAlphabet();
    }
    private IEnumerator Addforce()
    {
        blChangeLane = false;
        SoundManager.Instance.PlaySound(SoundType.EnergyTouch);
        yield return Constant.wait1s.Wait();
        car.SetState(typeCar, 4, playcar);
        yield return stateCar4.Wait();
        car.SetState(typeCar, 5, playcar);
        SoundManager.Instance.PlaySound(SoundType.EnergyPowerUp);
        car.Move(3, transform.position, new Vector3(4, transform.position.y, transform.position.z));
        Notify(speedAddforceBackGround);
        StartCoroutine(MapManager.Instance.EndingGame());
    }
    public void EndingGame()
    {
        Notify(stopBackGround);
        car.Move(0.5f, transform.position, new Vector3(22, transform.position.y, 0));
    }
}
