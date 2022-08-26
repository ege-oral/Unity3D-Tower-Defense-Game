using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    [SerializeField] int enemyMaxHealth = 5;
    [SerializeField] int enemyCurrentHealth = 0;

    private void OnEnable() 
    {
        enemyCurrentHealth = enemyMaxHealth;
    }
    void OnParticleCollision(GameObject other) 
    {
        Hit();
    }

    private void Hit()
    {
        enemyCurrentHealth -= 1;
        if(enemyCurrentHealth <= 0)
        {
            this.gameObject.SetActive(false);
            // DON'T FORGET THE CHANGE POSITION
            transform.position = new Vector3(0f,0f,0f);
        }
    }
}
