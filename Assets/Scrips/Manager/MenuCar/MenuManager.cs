using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuManager : Singleton<MenuManager>
{
    [SerializeField] public List<GameObject> lstCar;
    public GameObject selectedCar;

    ILoadCar loadCar;
    IDisableButton disableButton;
    IPopupCar popupCar;
    IScaleCar scaleCar;
    ISetActiveText setActiveText;
    IStartFadein startFadein;

    private bool blOneTimeDisable = true;
    public bool blSelectedCar;
    public bool blactiveText;
    public bool blCompletedScale;

    void Start()
    {
        MusicManager.Instance.PlayMusic(MusicType.WaittingSelectedCar);
        loadCar = GetComponent<ILoadCar>();
        disableButton = GetComponent<IDisableButton>();
        popupCar = GetComponent<IPopupCar>();
        scaleCar = GetComponent<IScaleCar>();
        setActiveText = GetComponent<ISetActiveText>();
        startFadein = GetComponent<IStartFadein>();
        loadCar.LoadCar();
    }

    private void Update()
    {
        if(blactiveText)
        {
            blactiveText = false;
            setActiveText.EnableText();
        }

        if (blSelectedCar)
        {
            if (blOneTimeDisable)
            {
                SoundManager.Instance.PlaySound(SoundType.GreatChoice);
                blOneTimeDisable = false;
                disableButton.DisableButton();
                setActiveText.DisableText();
                popupCar.PopupCar();
                scaleCar.ScaleCar();
            }
        }
        
        if(blCompletedScale)
        {
            blCompletedScale = false;
            StartCoroutine(Game());
        }    
    }

    private IEnumerator Game()
    {
        startFadein.StartFadein();
        yield return new WaitForSeconds(2f);
        StorageTypeCarManager.Instance.GetListCar();
        SceneManager.LoadScene("PlayGame");
    }
}
