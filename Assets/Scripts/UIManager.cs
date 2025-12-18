using System.Security.Cryptography;
using System.Threading.Tasks;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button btnSetting;
    public Button btnResume;
    public Button btnReplay;
    public Button btnReplayGameOver;
    public Button btnHelp;
    public Button btnBackGroundSound;
    public Button btnEffectSound;
    public Button btnHome;
    public Button btnHomeGameOver;
    public GameObject imagePause;
    public GameObject imageGameOver;
    public GameObject turnOffBackGroundSound;
    public GameObject turnOffEffectSound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(btnSetting != null)
        {
            btnSetting.onClick.AddListener(OpenImagePause);
        }
        if(btnResume != null)
        {
            btnResume.onClick.AddListener(Resume);
        }
        if(btnReplay != null)
        {
            btnReplay.onClick.AddListener(Replay);
        }
        if(btnHelp != null)
        {
            btnHelp.onClick.AddListener(Help);
        }
        if(btnBackGroundSound != null)
        {
            btnBackGroundSound.onClick.AddListener(BackgroundSound);
        }
        if(btnEffectSound != null)
        {
            btnEffectSound.onClick.AddListener(EffectSound);
        }
        if(btnReplayGameOver != null)
        {
            btnReplayGameOver.onClick.AddListener(Replay);
        }
        if(btnHome != null)
        {
            btnHome.onClick.AddListener(BackHome);
        }
        if(btnHomeGameOver != null)
        {
            btnHomeGameOver.onClick.AddListener(BackHome);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(BoxGameOver.instant.isGameOver)
        {
            imageGameOver.SetActive(true);
        }
    }
    private void OpenImagePause()
    {
        if(!BoxGameOver.instant.isGameOver)
        {
            imagePause.SetActive(true);
            GameManager.instance.isPause = true;
        }
    }
    private void Resume()
    {
        GameManager.instance.isPause = false;
        imagePause.SetActive(false);
        InputManager.instance.SetNextTime(0.2f);
    }
    private void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        GameManager.instance.isPause = false;
        imagePause.SetActive(false);
        InputManager.instance.SetNextTime(0.2f);
    }
    private void Help()
    {
        if(!BoxGameOver.instant.isGameOver)
        {
            AdsManager.instance.ShowInterstitial();
            GameManager.instance.isHelpMode = true;
        }
    }
    private void BackgroundSound()
    {
        if(!SoundManager.instance.isPlayBackgoundSound)
        {
            SoundManager.instance.PlayBackgroundSound();
            turnOffBackGroundSound.SetActive(false);
        }
        else
        {
            SoundManager.instance.StopBackGroudSound();
            turnOffBackGroundSound.SetActive(true);
        }
    }
    private void EffectSound()
    {
        SoundManager.instance.isPlayMergeSound = !SoundManager.instance.isPlayMergeSound;
        if(!SoundManager.instance.isPlayMergeSound) turnOffEffectSound.SetActive(true);
        else turnOffEffectSound.SetActive(false);
    }
    private void BackHome()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
