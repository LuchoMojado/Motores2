using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriveController : SteeringAgent
{
    Gyroscope _gyro;

    public CoinsAndTime cAndT;

    [SerializeField] float _turnSensibility, _boostDepletionRate, _maxBoost;

    float _currentRotation, _resetCount = 0, _boost = 0;

    public bool accelerating = false, boosting = false, breaking = false;
    
    private void Awake()
    {
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
            _currentRotation = Mathf.Clamp(_currentRotation + rotationRate, -160, 160);
        }
        else
        {
            _resetCount += Time.deltaTime;
            if (_resetCount >= 0.6f && Mathf.Abs(_currentRotation) < 20)
            {
                _currentRotation = 0;
            }
        }

        //print(_currentRotation);

        if (accelerating && _acceleration <= _speed)
        {
            ChangeAcceleration(0.75f);
        }
        else if (breaking)
        {
            if (_acceleration > 0)
            {
                ChangeAcceleration(-2);
            }
            else
            {
                ChangeAcceleration(-0.5f, _speed * -0.5f);
            }
        }
        else if (boosting && _boost > 0)
        {
            ChangeBoost(-Time.deltaTime * _boostDepletionRate);
            ChangeAcceleration(2.5f, 0, _speed * 1.5f);
        }
        else
        {
            if (_acceleration > 0)
            {
                ChangeAcceleration(-0.5f);
            }
            else if (_acceleration < 0)
            {
                ChangeAcceleration(0.5f, _speed * -0.5f);
            }
        }

        print(_acceleration);

        var dir = transform.forward + transform.right * -_currentRotation * _turnSensibility;
        dir.Normalize();

        AddForce(dir);
        
        Move();
    }

    public void ChangeBoost(float amount)
    {
        _boost = Mathf.Clamp(_boost + amount, 0, _maxBoost);
    }
}
