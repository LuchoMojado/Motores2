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

            float targetDist;

            while ((targetDist = Vector3.Distance(transform.position, targetPos)) > 2)
            {
                if (targetDist > 15)
                {
                    ChangeAcceleration(0.75f, 0);
                }
                else if (_acceleration < 1)
                {
                    ChangeAcceleration(1, 0);
                }
                else
                {
                    ChangeAcceleration(-1, 0);
                }

                AddForce(Seek(targetPos));
                
                yield return null;
            }
        }

        StartCoroutine(DriveWaypoints());
    }
}
//<>new Vector3(waypoints[i].position.x, transform.position.y, waypoints[i].position.z)