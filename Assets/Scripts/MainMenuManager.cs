using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public Button buttonPlay;
    public Button buttonExit;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(buttonPlay != null) buttonPlay.onClick.AddListener(Play);
        if(buttonExit != null) buttonExit.onClick.AddListener(Exit);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Play()
    {
        SceneManager.LoadScene("PlayScene");
    }
    public void Exit()
    {
        Application.Quit();
    }
}
