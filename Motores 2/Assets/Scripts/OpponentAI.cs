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

            float targetDist = Vector3.Distance(transform.position, targetPos);

            while (targetDist > 1.5f)
            {
                if (targetDist > 10)
                {
                    _speed = 3;
                }
                else
                {
                    _speed = 2;
                }

                ChangeAcceleration(1, 0);

                AddForce(Seek(targetPos));
                
                yield return null;
            }
        }
    }
}
//<>new Vector3(waypoints[i].position.x, transform.position.y, waypoints[i].position.z)