using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriveController : SteeringAgent
{
    Gyroscope _gyro;

    [SerializeField] float _turnSensibility, _breakStr;

    protected override void Awake()
    {
        base.Awake();

        _gyro = Input.gyro;
        _gyro.enabled = true;
    }

    private void Update()
    {
        print(_gyro.rotationRate.z);
        
        if (true/*aprieto el boton de acelerar*/)
        {
            var dir = transform.forward + new Vector3(_gyro.rotationRate.z * _turnSensibility, 0);
            dir.Normalize();
        
            AddForce(CalculateSteering(dir * _speed));
        }
        else if (true/*aprieto el boton de frenar*/)
        {
            AddForce(-transform.forward * _breakStr);
        }
        
        Move();
    }
}
