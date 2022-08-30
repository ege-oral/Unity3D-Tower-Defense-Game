using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] Tower defenseTower;
    [SerializeField] bool isPlaceable;

    public bool IsPlaceable{ get { return isPlaceable; } } // Property.

    GridManager gridManager;
    Pathfinder pathfinder;
    Vector2Int coordinates = new Vector2Int();


    
    private void Awake() 
    {
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<Pathfinder>();
    }

    private void Start() 
    {
        if(gridManager != null)
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);

            if(!isPlaceable)
            {
                gridManager.BlockMethod(coordinates);
            }
        }
    }

    private void OnMouseDown() 
    {
        if(gridManager.GetNode(coordinates).isWalkable && !pathfinder.WillBlockPath(coordinates))
        {
            bool isSuccessful = defenseTower.CreateTower(defenseTower, new Vector3(transform.position.x, 0.5f, transform.position.z));
            if(isSuccessful)
            {
                gridManager.BlockMethod(coordinates);
                pathfinder.NotifyReceivers();
            }  
        }
    }
}
