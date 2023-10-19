using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriveController : SteeringAgent
{
    Gyroscope _gyro;
    
    [SerializeField] float _turnSensibility, _breakStr, _accChangeRate;

    float _currentRotation, _resetCount = 0, _acceleration;

    public bool accelerating = false, breaking = false;
    
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
            _currentRotation = Mathf.Clamp(_currentRotation + rotationRate, -150, 150);
        }
        else
        {
            _resetCount += Time.deltaTime;
            if (_resetCount >= 0.7f && Mathf.Abs(_currentRotation) < 20)
            {
                _currentRotation = 0;
            }
        }
        


        print(_currentRotation);

        if (accelerating)
        {
            //Accelerate();
            ChangeAcceleration(1);
        }
        else if (breaking)
        {
            //Break();
            ChangeAcceleration(-2);
        }
        else
        {
            ChangeAcceleration(-0.5f);
        }

        var dir = transform.forward + transform.right * -_currentRotation * _turnSensibility;
        dir.Normalize();

        AddForce(dir * _acceleration);
        _velocity *= _breakStr;
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

    public void ChangeAcceleration(float multiplier)
    {
        _acceleration = Mathf.Clamp(_acceleration + Time.deltaTime * _accChangeRate * multiplier, 0, _speed);
    }

    public void Accelerate()
    {
        var dir = transform.forward + transform.right * -_currentRotation * _turnSensibility;
        dir.Normalize();

        AddForce(dir * _speed);
    }

    public void Break()
    {
        /*var dir = transform.right * -_currentRotation * _turnSensibility;
        AddForce(dir);*/

        _velocity *= _breakStr;
        if (0.1f >= _velocity.magnitude)
        {
            _velocity = Vector3.zero;
        }
    }


    public CoinsAndTime cAndT;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Coin"))
        {
            cAndT.TakeCoin();
            Destroy(other.gameObject);
        }
    }
}
