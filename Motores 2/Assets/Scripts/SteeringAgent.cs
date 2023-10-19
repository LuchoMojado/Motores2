using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SteeringAgent : MonoBehaviour
{
    protected Vector3 _velocity = Vector3.zero;

    [SerializeField] protected float _maxSpeed, _maxForce, _speed, _accChangeRate, _breakStr;

    protected float _acceleration = 0;

    protected virtual void Move()
    {
        transform.position += _velocity * Time.deltaTime * _acceleration;
        if (_acceleration != 0) 
            transform.forward = _velocity;
    }

    protected Vector3 Seek(Vector3 targetPos)
    {
        Vector3 desired = targetPos - transform.position;
        desired.Normalize();
        desired *= _maxSpeed;

        Vector3 steering = desired - _velocity;
        steering = Vector3.ClampMagnitude(steering, _maxForce * Time.deltaTime);

        return steering;
    }

    protected Vector3 CalculateSteering(Vector3 desired)
    {
        return Vector3.ClampMagnitude(desired - _velocity, _maxForce * Time.deltaTime);
    }

    protected void AddForce(Vector3 force)
    {
        _velocity = Vector3.ClampMagnitude(_velocity + force, _maxSpeed);
    }

    public void ChangeAcceleration(float multiplier, float min)
    {
        _acceleration = Mathf.Clamp(_acceleration + Time.deltaTime * _accChangeRate * multiplier, min, _speed);
    }
}
