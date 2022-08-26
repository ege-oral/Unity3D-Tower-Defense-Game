using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] float spawnTimer = 1f;

    private GameObject[] pool;
    [SerializeField] int poolSize = 5;

    void Awake()
    {
        PopulatePool();
    }

    void Start()
    {
        //InvokeRepeating("InstantiateEnemy", 0f, spawnTimer);   
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void InstantiateEnemy()
    {
        Instantiate(enemy, transform.position, Quaternion.identity);
    }

    private void PopulatePool()
    {
        pool = new GameObject[poolSize];

        for(int i = 0; i < poolSize; i++)
        {
            pool[i] = Instantiate(enemy, transform.position, Quaternion.identity);
            pool[i].SetActive(false);
        }
    }
}
