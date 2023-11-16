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
    private Vector3 startPosition;

    private Car car;
    private IStartCownDown startCownDown;

    string typeCar;

    private bool blChangeLane = false;

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
        playcar = Instantiate(carparttern, transform.position, Quaternion.identity);
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

        SideRoadManager.Instance.StartGo();
        yield return new WaitForSeconds(0.25f);
        AutoManager1.Instance.StartPlay();
        yield return new WaitForSeconds(0.25f);
        AutoManager2.Instance.StartPlay();
        NotifyNormal();
        car.SetState(typeCar, 1, playcar);
        yield return new WaitForSeconds(2.5f);
        SoundManager.Instance.PlaySound(SoundType.Guiding);
        guidFinger = Instantiate(prefabguidFinger, new Vector3(7, -2, 0), Quaternion.identity);
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
        }
        else if (Mathf.Abs(positionY - newPosition.y) == 2)
        {
            StartCoroutine(FirstGuiding());
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

    private IEnumerator FirstGuiding()
    {
        if (guidFinger != null)
        {
            SoundManager.Instance.StopSound();
            Destroy(guidFinger);
            yield return new WaitForSeconds(0.3f);
            SoundManager.Instance.PlaySound(SoundType.NiceDriving);
            yield return new WaitForSeconds(4);
            MapManager.Instance.GenerateAlphabet();
        }
    }

    public void DisableChangeLane()
    {   
        blChangeLane = false;
    }

    private void OnTriggerEnter2D(Collider2D alphabet)
    {
        if (alphabet.tag != "Addforce")
        {
            MapManager.Instance.TriggerAlphabet();
            StartCoroutine(StateTrigger());
        }
        else
        {
            Destroy(alphabet);
            StartCoroutine(Addforce());
        }
    }
    private IEnumerator Addforce()
    {
        SoundManager.Instance.PlaySound(SoundType.EnergyTouch);
        yield return new WaitForSeconds(1.5f);
        car.SetState(typeCar, 4, playcar);
        yield return new WaitForSeconds(0.3f);
        car.SetState(typeCar, 5, playcar);
        SoundManager.Instance.PlaySound(SoundType.EnergyPowerUp);
        car.Move(4, transform.position, new Vector3(22, transform.position.y, transform.position.z));

    }

    private IEnumerator StateTrigger()
    {
        blChangeLane = false;
        startPosition = transform.position;
        yield return new WaitForSeconds(1f);
        car.SetState(typeCar, 2, playcar);
        yield return new WaitForSeconds(0.75f);
        car.Move(3, transform.position, new Vector3(0, startPosition.y, startPosition.z));
        NotifyTrigger();
        yield return new WaitForSeconds(3);
        car.SetState(typeCar, 3, playcar);
        yield return new WaitForSeconds(1.5f);
        car.SetState(typeCar, 1, playcar);
        car.Move(2, transform.position, startPosition);
        yield return new WaitForSeconds(2);
        NotifyNormal();
        blChangeLane = true;
        yield return new WaitForSeconds(1.5f);
        MapManager.Instance.GenerateAlphabet();
    }   
}
