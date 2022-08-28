using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{

    [SerializeField] Transform weapon;
    [SerializeField] ParticleSystem arrowParticles;
    [SerializeField] float towerRange = 15f;
    Transform target;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        FindClosestTarget();
        AimWeapon();
    }

    private void FindClosestTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform closestTarget = null;
        float maxDistance = Mathf.Infinity;

        foreach(Enemy enemy in enemies)
        {
            float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);
            if(targetDistance < maxDistance)
            {
                maxDistance = targetDistance;
                closestTarget = enemy.transform;
            }
        }
        target = closestTarget;
    }

    private void AimWeapon()
    {
        try{
            float targetDistance = Vector3.Distance(transform.position, target.position);
            weapon.LookAt(target);
            Attack(targetDistance <= towerRange);
        }
        catch{
            // If there is no target pass.
        }
        
        
    }

    private void Attack(bool isActive)
    {
       var emissionModule = arrowParticles.emission;
       emissionModule.enabled = isActive;
    }
}
