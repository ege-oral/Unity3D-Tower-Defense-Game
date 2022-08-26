using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    [SerializeField] int enemyHealth = 5;

    void OnParticleCollision(GameObject other) 
    {
        enemyHealth -= 1;
        if(enemyHealth <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }
}
