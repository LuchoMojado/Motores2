using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentAI : SteeringAgent
{
    [SerializeField] Transform[] waypoints;
    int _currentWaypoint = 0;
    [SerializeField] float _viewRange;
    [SerializeField] LayerMask _obstacleLM;

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
    }

    protected bool ObstacleAvoidance(out Vector3 v)
    {
        bool lRaycast = Physics.Raycast(transform.position - transform.right * 0.5f, transform.forward, _viewRange, _obstacleLM);
        bool rRaycast = Physics.Raycast(transform.position + transform.right * 0.5f, transform.forward, _viewRange, _obstacleLM);
        /*bool lRaycast = Physics.Linecast(transform.position - transform.right * 0.5f, transform.forward * _viewRange, _obstacleLM);
        bool rRaycast = Physics.Linecast(transform.position + transform.right * 0.5f, transform.forward * _viewRange, _obstacleLM);*/

        if (lRaycast && rRaycast)
        {
            ChangeAcceleration(-10, -_speed * 0.5f);
            v = Vector3.zero;
            return true;
        }
        else if (lRaycast)
        {
            v = CalculateSteering(transform.right * _maxSpeed);
            return true;
        }
        else if (rRaycast)
        {
            v = CalculateSteering(-transform.right * _maxSpeed);
            return true;
        }

        v = Vector3.zero;
        return false;
    }
}
//<>new Vector3(waypoints[i].position.x, transform.position.y, waypoints[i].position.z)