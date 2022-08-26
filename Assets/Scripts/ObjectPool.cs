using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject enemy;

    private GameObject[] pool;
    [SerializeField] int poolSize = 5;

    
    [SerializeField] float spawnTimer = 2f;
    float timer;
    bool canSpawn = true;

    void Awake()
    {
        
        PopulatePool();
    }

    void Start()
    {
        timer = spawnTimer;
    }

    // Update is called once per frame
    void Update()
    {


        EnableObjectInPool();
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            canSpawn = true;
            timer = spawnTimer;
        }

    }



    private void PopulatePool()
    {
        pool = new GameObject[poolSize];

        for(int i = 0; i < poolSize; i++)
        {
            pool[i] = Instantiate(enemy, transform.position, Quaternion.identity);
            pool[i].transform.parent = gameObject.transform;
            pool[i].SetActive(false);
        }
    }

    private void EnableObjectInPool()
    {
        foreach(GameObject enemy in pool)
        {
            if(!enemy.activeSelf && canSpawn)
            {
                enemy.SetActive(true);
                canSpawn = false;   
            }
        }
    }
}
