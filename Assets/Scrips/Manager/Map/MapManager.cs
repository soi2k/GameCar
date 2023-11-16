using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{   
    public static MapManager Instance { get; private set; }
    [SerializeField] private List<GameObject> lstAlphabet;
    [SerializeField] private List<GameObject> lstMotionAudience;
    [SerializeField] private GameObject preAddforce;
    private GameObject road;
    private GameObject cellWord;
    private GameObject sideRoad;

    private GameObject alphabet;
    private List<float> lstPstYAlphabet = new List<float> { -4.4f, -2.4f, -0.4f };

    private int numberMiss = 0;
    private int passCarNumbers = 0;
    private int indexListWord = 0;

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
        if (indexListWord > 2) return;
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
        numberMiss += 1;
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
        }
        else if(numberMiss == 2)
        {
            AutoManager2.Instance.MoveForWard();
        }
        yield return new WaitForSeconds(2.5f);
        alphabet.transform.SetParent(road.transform);
        if (numberMiss == 2)
        {
            yield return new WaitForSeconds(1.5f);
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
            PlayerManager.Instance.DisableChangeLane();
        }
    }

    public void TriggerAlphabet()
    {
        StartCoroutine(Trigger());
    }
    public IEnumerator Trigger()
    {   
        passCarNumbers += 1;

        GameObject audienceHappy = Instantiate(lstMotionAudience[0], new Vector3(21, 0, 0), Quaternion.identity);
        audienceHappy.transform.SetParent(sideRoad.transform);

        alphabet.transform.SetParent(this.transform);
        alphabet.GetComponent<IMoveToTarget>().MoveToTarget(0.5f, alphabet.transform.position, lstAlphabet[indexListWord].transform.position);
        SoundManager.Instance.PlaySound(SoundType.ItemTouch);
        SoundManager.Instance.PlaySound(SoundType.CrowdCheering);
        yield return new WaitForSeconds(0.75f);
        alphabet.GetComponent<AudioSource>().Play();
        indexListWord += 1;

        if (passCarNumbers == 1)
        {
            yield return new WaitForSeconds(1f);
            AutoManager1.Instance.MoveBackWard();
        }
        else if (passCarNumbers == 2)
        {
            yield return new WaitForSeconds(1f);
            AutoManager2.Instance.MoveBackWard();
        }
        
        if(indexListWord == 3)
        {
            StartCoroutine(WinGame());
        }
    }
    
    private IEnumerator WinGame()
    {
        yield return new WaitForSeconds(8f);
        GameObject addforce = Instantiate(preAddforce, new Vector3(19, 0, 0), Quaternion.identity);
        addforce.transform.SetParent(road.transform);
    }    

}
