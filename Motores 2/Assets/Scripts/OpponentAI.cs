using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentAI : SteeringAgent
{
    [SerializeField] Transform[] waypoints;
    int _currentWaypoint = 0;

    void Update()
    {
        if (ObstacleAvoidance(out Vector3 obs))
        {
            AddForce(obs);
        }
        else if (_currentWaypoint < waypoints.Length)
        {
            Vector3 targetPos = new Vector3(waypoints[_currentWaypoint].position.x, transform.position.y, waypoints[_currentWaypoint].position.z);

            float targetDist;

            if ((targetDist = Vector3.Distance(transform.position, targetPos)) > 2)
            {
                if (targetDist > 15)
                {
                    ChangeAcceleration(0.75f);
                }
                else if (_acceleration < 1)
                {
                    ChangeAcceleration(1);
                }
                else
                {
                    ChangeAcceleration(-1);
                }

                AddForce(Seek(targetPos));
            }
            else
            {
                _currentWaypoint++;

                if (_currentWaypoint >= waypoints.Length)
                {
                    _currentWaypoint = 0;
                }
            }
        }

        Move();
        Gravity();
    }
}
//<>new Vector3(waypoints[i].position.x, transform.position.y, waypoints[i].position.z)