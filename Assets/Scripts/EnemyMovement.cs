using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField] List<Waypoint> path = new List<Waypoint>();
    [SerializeField] float waitTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DisplayPath());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DisplayPath()
    {
        foreach(Waypoint waypoint in path)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = new Vector3(waypoint.transform.position.x, 0f, waypoint.transform.position.z);
            float travelDistance = 0f;

            transform.LookAt(endPosition);
            
            while(travelDistance < 1f)
            {
                travelDistance += Time.deltaTime;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelDistance);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
