using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    List<Node> path = new List<Node>();
    


    [SerializeField] [Range(0f, 5f)] float enemySpeed = 2f;

    Enemy enemy;
    GridManager gridManager;
    Pathfinder pathfinder;

    private void Awake() 
    {
        enemy = GetComponent<Enemy>();    
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<Pathfinder>();
    }

    void OnEnable()
    {
        FindPath();
        ReturnToStart();
        StartCoroutine(FollowPath());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FindPath()
    { 
        path.Clear();
        path = pathfinder.GetNewPath();
    }

    private void FinishPath()
    {
        enemy.PenaltyGold();
        this.gameObject.SetActive(false);
    }

    public void ReturnToStart()
    {
        // See what will happen
        transform.position = gridManager.GetPositionFromCoordinates(pathfinder.StartCoordinates);
    }

    IEnumerator FollowPath()
    {
        for(int i = 0; i < path.Count; i++)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = gridManager.GetPositionFromCoordinates(path[i].coordinates);
            float travelPercent = 0f;
            transform.LookAt(endPosition);

            while(travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * enemySpeed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }
        }
        FinishPath();
    }
}
