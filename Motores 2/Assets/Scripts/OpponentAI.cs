using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentAI : SteeringAgent
{
    [SerializeField] Transform[] waypoints;

    private void Start()
    {
        StartCoroutine(DriveWaypoints());
    }

    void Update()
    {
        Move();
    }

    IEnumerator DriveWaypoints()
    {
        for (int i = 0; i < waypoints.Length; i++)
        {
            Vector3 targetPos = new Vector3(waypoints[i].position.x, transform.position.y, waypoints[i].position.z);

            while (Vector3.Distance(transform.position, targetPos) > 1.5f)
            {
                AddForce(Seek(targetPos) * _speed);
                
                yield return null;
            }
        }
    }
}
//<>new Vector3(waypoints[i].position.x, transform.position.y, waypoints[i].position.z)