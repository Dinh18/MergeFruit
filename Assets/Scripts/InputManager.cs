using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;
    public float minX = -2.5f; 
    public float maxX = 2.5f;
    private Camera mainCamera;
    private float nextTime = 0;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.isPause || GameManager.instance.isHelpMode || Time.time < nextTime || BoxGameOver.instant.isGameOver) return;
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
            {
                return;
            }
            Vector3 touchPos = touch.position;
            touchPos.z = -mainCamera.transform.position.z;
            Vector3 worldPos = mainCamera.ScreenToWorldPoint(touchPos);
            switch(touch.phase)
            {
                case TouchPhase.Began:
                case TouchPhase.Moved:
                case TouchPhase.Stationary:
                    if(worldPos.y < 2.5) MoveSpawner(worldPos.x);
                    break;
                case TouchPhase.Ended:
                    DropFruit.instant.Drop();
                    break;
                case TouchPhase.Canceled:
                    break;
            }
        }
    }
    public void SetNextTime(float time)
    {
        nextTime = Time.time + time;
    }
    void MoveSpawner(float targetX)
    {
        float clampedX = Mathf.Clamp(targetX, minX, maxX);

        if(DropFruit.instant.fruit != null)
        {
            Vector3 newPos = DropFruit.instant.fruit.transform.position;
            newPos.x = clampedX;
            DropFruit.instant.fruit.transform.position = newPos;
        }
    }
}
