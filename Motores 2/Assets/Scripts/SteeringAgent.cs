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
        bool walledFront = CheckCollision(out bool walledBack);

        if (walledFront)
        {
            if (_acceleration > 0)
            {
                _acceleration = 0;
            }
        }
        else if (walledBack)
        {
            if (_acceleration < 0)
            {
                _acceleration = 0;
            }
        }

        if (GroundedCheck(out RaycastHit rayHit))
        {
            if (rayHit.collider.gameObject.layer == 13)
            {
                transform.position += _velocity * Time.deltaTime * _acceleration * 0.6f;
            }
            else
            {
                transform.position += _velocity * Time.deltaTime * _acceleration;
            }
        }

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

    protected bool CheckCollision(out bool wallBack)
    {
        bool lRaycast = Physics.Raycast(transform.position - transform.right * 0.4f, transform.forward, 2.3f, _obstacleLM);
        bool rRaycast = Physics.Raycast(transform.position + transform.right * 0.4f, transform.forward, 2.3f, _obstacleLM);
        bool lBackRaycast = Physics.Raycast(transform.position - transform.right * 0.4f, -transform.forward, 2.3f, _obstacleLM);
        bool rBackRaycast = Physics.Raycast(transform.position + transform.right * 0.4f, -transform.forward, 2.3f, _obstacleLM);

        if (lRaycast || rRaycast)
        {
            wallBack = false;
            return true;
        }
        else if(lBackRaycast || rBackRaycast)
        {
            wallBack = true;
            return false;
        }

        wallBack = false;
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

    public bool GroundedCheck()
    {
        return Physics.Raycast(transform.position, -transform.up, 1);
    }

    public bool GroundedCheck(out RaycastHit hit)
    {
        bool grounded = Physics.Raycast(transform.position, -transform.up, out RaycastHit rayhit, 1);

        hit = rayhit;
        return grounded;
    }
}
