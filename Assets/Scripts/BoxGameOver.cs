using System;
using UnityEngine;

public class BoxGameOver : MonoBehaviour
{
    public static BoxGameOver instant;
    private float count = 0;
    private float timeOver = 5f;
    public bool isGameOver = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instant = this;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(count);
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if(!collision.gameObject.CompareTag("Border") && collision.gameObject.GetComponent<Rigidbody2D>().linearVelocityY > 0 && !isGameOver)
        {
            count+=Time.deltaTime;
            if(count >= timeOver)
            {
                isGameOver = true;
                AdsManager.instance.ShowInterstitial();
            }
        }
    }
}
