using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SteeringAgent : MonoBehaviour
{
    protected Vector3 _velocity = Vector3.zero;

    [SerializeField] protected float _maxSpeed, _maxForce, _speed, _accChangeRate, _breakStr, _viewRange;

    [SerializeField] LayerMask _obstacleLM;

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

    protected bool ObstacleAvoidance()
    {
        bool lRaycast = Physics.Raycast(transform.position - transform.right * 0.5f, transform.forward, _viewRange, _obstacleLM);
        bool rRaycast = Physics.Raycast(transform.position + transform.right * 0.5f, transform.forward, _viewRange, _obstacleLM);

        if (lRaycast)
        {
            return true;
        }
        else if (rRaycast)
        {
            return true;
        }

        return false;
    }

    protected bool ObstacleAvoidance(out Vector3 v)
    {
        bool lRaycast = Physics.Raycast(transform.position - transform.right * 0.5f, transform.forward, _viewRange, _obstacleLM);
        bool rRaycast = Physics.Raycast(transform.position + transform.right * 0.5f, transform.forward, _viewRange, _obstacleLM);

        if (lRaycast && rRaycast)
        {
            ChangeAcceleration(-8, -_speed);
            v = Vector3.zero;
            return true;
        }
        else if (lRaycast)
        {
            v = CalculateSteering(-transform.forward * _maxSpeed);
            return true;
        }
        else if (rRaycast)
        {
            v = CalculateSteering(transform.forward * _maxSpeed);
            return true;
        }

        v = Vector3.zero;
        return false;
    }

    protected Vector3 CalculateSteering(Vector3 desired)
    {
        return Vector3.ClampMagnitude(desired - _velocity, _maxForce * Time.deltaTime);
    }

    protected void AddForce(Vector3 force)
    {
        _velocity = Vector3.ClampMagnitude(_velocity + force, _maxSpeed);
    }

    public void ChangeAcceleration(float multiplier)
    {
        _acceleration = Mathf.Clamp(_acceleration + Time.deltaTime * _accChangeRate * multiplier, 0, _speed);
    }

    public void ChangeAcceleration(float multiplier, float min)
    {
        _acceleration = Mathf.Clamp(_acceleration + Time.deltaTime * _accChangeRate * multiplier, min, _speed);
    }

    public void ChangeAcceleration(float multiplier, float min, float max)
    {
        _acceleration = Mathf.Clamp(_acceleration + Time.deltaTime * _accChangeRate * multiplier, min, max);
    }

    public void Gravity()
    {
        if (!Physics.Raycast(transform.position, -transform.up, out RaycastHit ray, 0.5f))
        {
            transform.position -= -transform.up * Time.deltaTime * 0.5f;
        }
    }
}
