using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] bool isPlaceable;
    [SerializeField] GameObject defenseTower;

    private void OnMouseDown() 
    {
        if(isPlaceable)
        {
            Instantiate(defenseTower, new Vector3(transform.position.x, 0.5f, transform.position.z), Quaternion.identity);
            isPlaceable = false;
        }  
    }
}
