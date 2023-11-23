using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : Subject
{   
    public static MapManager Instance { get; private set; }

    private List<float> lstPstYAlphabet = new List<float> { -4.4f, -2.4f, -0.4f };
    [SerializeField] private List<GameObject> lstAlphabet;
    [SerializeField] private List<GameObject> lstMotionAudience;
    [SerializeField] private GameObject preAddforce;
    [SerializeField] private GameObject preLine;
    [SerializeField] private GameObject preFlag;
    private GameObject road;
    private GameObject cellWord;
    private GameObject sideRoad;
    private GameObject alphabet;

    private Vector3 startPst = new Vector3(0, 0, 0);

    private int numberMiss = 0;
    private int passCarNumbers = 0;
    private int indexListWord = 0;

    private float wait0_75s = 0.75f;
    private float wait1s = 1f;
    private float wait1_5s = 1.5f;
    private float wait3s = 3f;
    private float wait4s = 4f;
    private float wait8s = 8f;

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
        road = GameObject.FindWithTag("Road");
        cellWord = GameObject.FindWithTag("CellWord");
        sideRoad = GameObject.FindWithTag("SideRoad");
    }

    public void GenerateAlphabet()
    {
        if (indexListWord == lstAlphabet.Count) return;
        StartCoroutine(Generate());
    }
    private IEnumerator Generate()
    {
        alphabet = Instantiate(lstAlphabet[indexListWord], new Vector3(17, lstPstYAlphabet[Random.Range(0, lstPstYAlphabet.Count)]), Quaternion.identity);
        alphabet.transform.SetParent(road.transform);
        yield return null;
    }

    public void WordMiss()
    {   
        if(numberMiss < 2) numberMiss += 1; 
        if (passCarNumbers > 0) passCarNumbers -= 1;
        StartCoroutine(Miss());
    }
    private IEnumerator Miss()
    {
        GameObject audienceSad = Instantiate(lstMotionAudience[1], new Vector3(21, 0, 0), Quaternion.identity);
        audienceSad.transform.SetParent(sideRoad.transform);

        alphabet.transform.SetParent(this.transform);
        alphabet.transform.position = new Vector3(17, lstPstYAlphabet[Random.Range(0, lstPstYAlphabet.Count)]);
        SoundManager.Instance.PlaySound(SoundType.CrowdDisappoint);

        // AutoCar vượt lên
        if(numberMiss == 1)
        {
            AutoManager1.Instance.MoveForWard();
            PlayerManager.Instance.SetActiveChangeLane(false);
        }
        else if(numberMiss == 2)
        {
            AutoManager2.Instance.MoveForWard();
            PlayerManager.Instance.SetActiveChangeLane(false);
        }
        yield return wait3s.Wait();
        PlayerManager.Instance.SetActiveChangeLane(true);
        alphabet.transform.SetParent(road.transform);
        if (numberMiss == 2)
        {
            yield return wait1_5s.Wait();
            alphabet.transform.SetParent(this.transform);
            alphabet.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        }
    }
    
    public void ChooseRightLane(float pstYAlphabet)
    {
        if(numberMiss == 2 && alphabet.transform.position.y == pstYAlphabet)
        {
            numberMiss = 0;
            alphabet.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
            alphabet.transform.SetParent(road.transform);
            PlayerManager.Instance.SetActiveChangeLane(false);
        }
    }

    public void TriggerAlphabet()
    {
        StartCoroutine(Trigger());
    }
    public IEnumerator Trigger()
    {   
        passCarNumbers += 1;
        if (numberMiss > 0) numberMiss -= 1;

        GameObject audienceHappy = Instantiate(lstMotionAudience[0], new Vector3(21, 0, 0), Quaternion.identity);
        audienceHappy.transform.SetParent(sideRoad.transform);

        alphabet.transform.SetParent(cellWord.transform);
        alphabet.GetComponent<IMoveToTarget>().MoveToTarget(0.5f, alphabet.transform.position, lstAlphabet[indexListWord].transform.position);
        SoundManager.Instance.PlaySound(SoundType.ItemTouch);
        SoundManager.Instance.PlaySound(SoundType.CrowdCheering);
        yield return wait1s.Wait();
        alphabet.GetComponent<AudioSource>().Play();
        indexListWord += 1;

        if (passCarNumbers == 1)
        {
            yield return wait1s.Wait();
            AutoManager1.Instance.MoveBackWard();
        }
        else if (passCarNumbers == 2)
        {
            yield return wait1s.Wait();
            AutoManager2.Instance.MoveBackWard();
        }
        
        if(indexListWord == 3)
        {
            StartCoroutine(WinGame());
        }
    }
    
    private IEnumerator WinGame()
    {
        yield return wait8s.Wait();
        GameObject addforce = Instantiate(preAddforce, new Vector3(19, 0, 0), Quaternion.identity);
        addforce.transform.SetParent(road.transform);
    }    

    public void EndingGame()
    {
        StartCoroutine(Ending());
    }

    private IEnumerator Ending()
    {
        if (passCarNumbers == 1)
        {
            yield return wait1s.Wait();
            AutoManager2.Instance.MoveBackWard();
        }

        yield return wait4s.Wait();
        GameObject line = Instantiate(preLine, new Vector3(19, 0, 0), Quaternion.identity);
        GameObject audienceHappy100 = Instantiate(lstMotionAudience[2], new Vector3(21, 0, 0), Quaternion.identity);
        line.transform.SetParent(road.transform);
        audienceHappy100.transform.SetParent(sideRoad.transform);

        yield return wait0_75s.Wait();
        PlayerManager.Instance.EndingGame();
        GameObject flag = Instantiate(preFlag);
        flag.GetComponent<IMoveToTarget>().MoveToTarget(0.5f, startPst, new Vector3(0, 3.5f, 0));
        SoundManager.Instance.PlaySound(SoundType.CrowdCheering);
        yield return wait1s.Wait();
        AutoManager1.Instance.MoveDestination();
        AutoManager2.Instance.MoveDestination();
        yield return wait3s.Wait();
        Notify(1);
        flag.GetComponent<IMoveToTarget>().MoveToTarget(0.5f, flag.transform.position, startPst);
        CellWordManager.Instance.FadeinCellWord();
    }
}
