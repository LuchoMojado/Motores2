using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriveController : SteeringAgent
{
    Gyroscope _gyro;

    [SerializeField] float _turnSensibility, _breakStr;

    float _currentRotation;

    protected override void Awake()
    {
        base.Awake();

        _gyro = Input.gyro;
        _gyro.enabled = true;
    }

    private void Update()
    {
        float rotationRate = _gyro.rotationRate.z;

        if (Mathf.Abs(rotationRate) >= 0.01f)
        {
            _currentRotation += rotationRate;
        }

        if (true/*aprieto el boton de acelerar*/)
        {
            var dir = transform.forward + new Vector3(_currentRotation /** _turnSensibility*/, 0);
            dir.Normalize();
        
            AddForce(CalculateSteering(dir * _speed));
        }
        else if (true/*aprieto el boton de frenar*/)
        {
            AddForce(-transform.forward * _breakStr);
        }
        
        Move();
    }

    //<>
}
