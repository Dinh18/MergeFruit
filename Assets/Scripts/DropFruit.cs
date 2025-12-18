using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class DropFruit : MonoBehaviour
{
    public static DropFruit instant;
    public GameObject fruit;
    public List<GameObject> fruits = new List<GameObject>();
    private bool isSpawning = false;
    void Awake()
    {
        instant = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnFruitRoutine(0f));
    }

    // Update is called once per frame
    void Update()
    {
        
        if(fruit == null && !isSpawning)
        {
            StartCoroutine(SpawnFruitRoutine(0.5f));
        }
    }
    public void Drop()
    {
        if(fruit != null)
        {
            fruit.GetComponent<Rigidbody2D>().isKinematic = false;
            fruit = null;
        }

    }
    private IEnumerator SpawnFruitRoutine(float delay)
    {
        isSpawning = true;

        yield return new WaitForSeconds(delay);

        int index = Random.Range(0, fruits.Count);
        
        fruit = Instantiate(fruits[index], transform.position, Quaternion.identity);
        
        fruit.GetComponent<Rigidbody2D>().isKinematic = true;

        isSpawning = false;
    }
}
