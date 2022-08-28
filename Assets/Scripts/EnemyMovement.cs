using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] List<Waypoint> path = new List<Waypoint>();
    [SerializeField] [Range(0f, 5f)] float enemySpeed = 2f;

    Enemy enemy;

    private void Start() 
    {
        enemy = GetComponent<Enemy>();    
    }

    void OnEnable()
    {
        ReturnToStart();
        StartCoroutine(EnemyPath());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator EnemyPath()
    { 
        foreach(Waypoint waypoint in path)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = new Vector3(waypoint.transform.position.x, 0f, waypoint.transform.position.z);
            float travelDistance = 0f;

            transform.LookAt(endPosition);

            while(travelDistance < 1f)
            {
                travelDistance += Time.deltaTime * enemySpeed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelDistance);
                yield return new WaitForEndOfFrame();
            }
        }
        FinishPath();
        
    }

    private void FinishPath()
    {
        enemy.PenaltyGold();
        this.gameObject.SetActive(false);
    }

    public void ReturnToStart()
    {
        transform.position = new Vector3(0f,0f,0f);
    }
}
