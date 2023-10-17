using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class SteeringAgent : MonoBehaviour
{
    protected Rigidbody _rb;

    protected Vector3 _velocity;

    [SerializeField] protected float _maxSpeed, _maxForce, _speed;

    protected void Move()
    {
        if (_velocity != Vector3.zero) transform.forward = _velocity;
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
        _rb.AddForce(_velocity);
    }
}
