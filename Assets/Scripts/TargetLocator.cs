using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{

    [SerializeField] Transform weapon;
    [SerializeField] Transform target;

    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<EnemyMovement>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        AimWeapon();
        //transform.Rotate(transform.rotation.x, 180f, transform.rotation.z);
    }

    private void AimWeapon()
    {
        weapon.LookAt(target);
        //weapon.transform.LookAt(target);
    }
}
