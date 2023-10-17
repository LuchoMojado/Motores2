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
            while (Vector3.Distance(transform.position, waypoints[i].position) > 1.5f)
            {
                AddForce(CalculateSteering(Seek(waypoints[i].position) * _speed));
                print(i);
                yield return null;
            }
        }
    }
}
//<>