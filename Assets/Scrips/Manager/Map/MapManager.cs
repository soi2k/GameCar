using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
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

    private Vector3 startPst = new (0, 0, 0);
    private Vector3 generationPst = new (21, 0, 0);
    private Vector3 targetFlagPst = new (0, 3.5f, 0);

    private int numberMiss = 0;
    private int passCarNumbers = 0;
    private int indexListWord = 0;

    private float timeMove = 0.5f;

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

    public IEnumerator WordMiss()
    {
        if (numberMiss < 2) numberMiss += 1;
        if (passCarNumbers > 0) passCarNumbers -= 1;

        GameObject audienceSad = Instantiate(lstMotionAudience[1], generationPst, Quaternion.identity);
        audienceSad.transform.SetParent(sideRoad.transform);

        alphabet.transform.SetParent(this.transform);
        alphabet.transform.position = new Vector3(17, lstPstYAlphabet[Random.Range(0, lstPstYAlphabet.Count)]);
        SoundManager.Instance.PlaySound(SoundType.CrowdDisappoint);

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
        yield return Constant.wait3s.Wait();
        PlayerManager.Instance.SetActiveChangeLane(true);
        alphabet.transform.SetParent(road.transform);
        if (numberMiss == 2)
        {
            yield return Constant.wait1_5s.Wait();
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

    public IEnumerator TriggerAlphabet()
    {   
        if (numberMiss > 0) numberMiss -= 1;

        GameObject audienceHappy = Instantiate(lstMotionAudience[0], generationPst, Quaternion.identity);
        audienceHappy.transform.SetParent(sideRoad.transform);

        alphabet.transform.SetParent(cellWord.transform);
        alphabet.GetComponent<IMoveToTarget>().MoveToTarget(timeMove, alphabet.transform.position, lstAlphabet[indexListWord].transform.position);
        SoundManager.Instance.PlaySound(SoundType.ItemTouch);
        SoundManager.Instance.PlaySound(SoundType.CrowdCheering);
        yield return Constant.wait1s.Wait();
        alphabet.GetComponent<AudioSource>().Play();
        indexListWord += 1;

        if (passCarNumbers == 0)
        {
            yield return Constant.wait1s.Wait();
            AutoManager1.Instance.MoveBackWard();
        }
        else if (passCarNumbers == 1)
        {
            yield return Constant.wait1s.Wait();
            AutoManager2.Instance.MoveBackWard();
        }
        
        if(indexListWord == lstAlphabet.Count)
        {
            StartCoroutine(WinGame());
        }
        passCarNumbers += 1;
    }
    
    private IEnumerator WinGame()
    {
        yield return Constant.wait8s.Wait();
        GameObject addforce = Instantiate(preAddforce, generationPst, Quaternion.identity);
        addforce.transform.SetParent(road.transform);
    }    

    public IEnumerator EndingGame()
    {
        if (passCarNumbers == 1)
        {
            yield return Constant.wait1s.Wait();
            AutoManager2.Instance.MoveBackWard();
        }

        yield return Constant.wait4s.Wait();
        GameObject line = Instantiate(preLine, generationPst, Quaternion.identity);
        GameObject audienceHappy100 = Instantiate(lstMotionAudience[2], generationPst, Quaternion.identity);
        line.transform.SetParent(road.transform);
        audienceHappy100.transform.SetParent(sideRoad.transform);

        yield return Constant.wait1s.Wait();
        PlayerManager.Instance.EndingGame();
        GameObject flag = Instantiate(preFlag);
        flag.GetComponent<IMoveToTarget>().MoveToTarget(timeMove, startPst, targetFlagPst);
        SoundManager.Instance.PlaySound(SoundType.CrowdCheering);
        yield return Constant.wait1s.Wait();
        AutoManager1.Instance.MoveDestination();
        AutoManager2.Instance.MoveDestination();
        yield return Constant.wait3s.Wait();
        MaskUI.Instance.EnableMask();
        flag.GetComponent<IMoveToTarget>().MoveToTarget(timeMove, flag.transform.position, startPst);
        CellWordManager.Instance.FadeinCellWord();
    }
    public void DestroyCellWord()
    {
        Destroy(cellWord);
    }
}
