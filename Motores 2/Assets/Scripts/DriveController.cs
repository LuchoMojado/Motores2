using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DriveController : SteeringAgent
{
    Gyroscope _gyro;
    Button _but;
    [SerializeField] float _turnSensibility, _breakStr;

    float _currentRotation, _resetCount = 0;

    bool accelerating = false;
    
    protected override void Awake()
    {
        base.Awake();
        //_but.
        _gyro = Input.gyro;
        _gyro.enabled = true;
    }

    private void Update()
    {
        float rotationRate = _gyro.rotationRate.z;

        if (Mathf.Abs(rotationRate) >= 0.05f)
        {
            _resetCount = 0;
            _currentRotation += rotationRate;
        }
        else
        {
            _resetCount += Time.deltaTime;
            if (_resetCount >= 0.9f && Mathf.Abs(_currentRotation) < 20)
            {
                _currentRotation = 0;
            }
        }

        print(_currentRotation * _turnSensibility);

        /*if (_velocity.sqrMagnitude > 0)
        {
            AddForce(transform.right * -_currentRotation * _turnSensibility);
        }*/
        
        Move();
    }

    public void Accelerate()
    {
        var dir = transform.forward + transform.right * -_currentRotation * _turnSensibility;
        dir.Normalize();

        AddForce(dir * _speed);
    }

    public void Break()
    {
        AddForce(-transform.forward * _breakStr);
    }
    
    //<>
}
