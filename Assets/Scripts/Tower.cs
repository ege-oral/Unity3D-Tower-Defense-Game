using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] int towerCost = 75;
    public int TowerCost { get { return towerCost; } }

    [SerializeField] float towerBuildTime = 0.5f;

    private void Start() 
    {
        
        StartCoroutine(BuildTower());
    }

    public bool CreateTower(Tower tower, Vector3 position)
    {
        Bank bank = FindObjectOfType<Bank>();
        if(bank == null) { return false; }

        if(bank.CurrentBalance >= towerCost)
        {
            Instantiate(tower.gameObject, position, Quaternion.identity);
            bank.Withdraw(towerCost);
            return true;
        }
        return false;
    }

    IEnumerator BuildTower()
    {
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(false);
            foreach(Transform grandchild in child)
            {
                grandchild.gameObject.SetActive(false);
            }
        }

        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(true);
            yield return new WaitForSeconds(towerBuildTime);
            foreach(Transform grandchild in child)
            {
                grandchild.gameObject.SetActive(true);
            }
        }
        
    }

}
