using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriveController : SteeringAgent
{
    Gyroscope _gyro;
    
    [SerializeField] float _turnSensibility, _breakStr;

    float _currentRotation, _resetCount = 0;

    public bool accelerating = false, breaking = false;
    
    protected override void Awake()
    {
        base.Awake();
        
        _gyro = Input.gyro;
        _gyro.enabled = true;
    }

    private void Update()
    {
        //_rb.MovePosition(-transform.up * Time.deltaTime);

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

        if (accelerating)
        {
            Accelerate();
        }
        else if (breaking)
        {
            Break();
        }
        
        Move();
    }

    /*protected override void Move()
    {
        if (!breaking)
        {
            base.Move();
        }
        else
        {
            transform.forward = -_velocity;
        }
    }*/

    public void Accelerate()
    {
        var dir = transform.forward + transform.right * -_currentRotation * _turnSensibility;
        dir.Normalize();

        AddForce(dir * _speed);
    }

    public void Break()
    {
        _rb.velocity *= _breakStr;
        if (0.1f >= _rb.velocity.magnitude)
        {
            _rb.velocity = Vector3.zero;
        }
    }
}
