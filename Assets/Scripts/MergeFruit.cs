using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class MergeFruit : MonoBehaviour
{
    public GameObject nextFruitPrefab;
    public int score;
    private bool hasMerge = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(hasMerge) return;

        if(this.gameObject.CompareTag(collision.gameObject.tag))
        {
            MergeFruit otherFruit = collision.gameObject.GetComponent<MergeFruit>();
            if(otherFruit != null && otherFruit.hasMerge) return;

            if(GetInstanceID() > collision.gameObject.GetInstanceID())
            {
                MergeLogic(collision.gameObject);
            }  
        }
    }
    private void MergeLogic(GameObject otherFruitObj)
    {
        hasMerge = true;
        Vector3 spawnPos = (transform.position + otherFruitObj.transform.position) / 2;
        if (nextFruitPrefab != null)
        {
            SoundManager.instance.PlayMergeSound();
            Instantiate(nextFruitPrefab, spawnPos, Quaternion.identity);
            GameManager.instance.UpdateScore(score);
        }


        Destroy(otherFruitObj);
        Destroy(this.gameObject);
    }
}
