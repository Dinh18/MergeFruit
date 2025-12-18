
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int score = 0;
    public TextMeshProUGUI tmpScore;
    public bool isHelpMode = false;
    public bool isPause = false;
    public static GameManager instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
        tmpScore.text = "Score: 0";
    }

    // Update is called once per frame
    void Update()
    {
        if(isHelpMode && Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began)
            {
                DetectObject(touch.position);
            }
        }
    }

    public void UpdateScore(int s)
    {
        score += s;
        tmpScore.text = "Score: " + score.ToString();
    }

    private void DetectObject(Vector3 touchPosition)
    {
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(touchPosition);

        RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

        if(hit.collider != null)
        {
            GameObject clickedGameObject = hit.collider.gameObject;
            if(!clickedGameObject.CompareTag("Border"))
            {
                Destroy(clickedGameObject);
                isHelpMode = false;
                InputManager.instance.SetNextTime(0.2f);
            }
        }
    }
}
