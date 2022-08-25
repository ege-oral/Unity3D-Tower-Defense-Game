using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] GameObject defenseTower;
    [SerializeField] bool isPlaceable;

    public bool IsPlaceable{ get { return isPlaceable; } } // Property.

    private void OnMouseDown() 
    {
        if(isPlaceable)
        {
            Instantiate(defenseTower, new Vector3(transform.position.x, 0.5f, transform.position.z), Quaternion.identity);
            isPlaceable = false;
        }  
    }
}
