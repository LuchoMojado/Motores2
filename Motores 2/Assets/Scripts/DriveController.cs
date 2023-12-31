using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DriveController : SteeringAgent
{
    public string Class;
    Gyroscope _gyro;

    [SerializeField] Image _boostBar;
    [SerializeField] Slider _sensibilitySlider;

    public CoinsAndTime cAndT;

    [Range(0.15f, 0.4f)] float _turnSensibility;
    [SerializeField] float _boostDepletionRate, _maxBoost;
    [SerializeField] Animator _anim;

    float _currentRotation, _resetCount = 0, _boost = 0;

    public bool accelerating = false, boosting = false, breaking = false;
    
    private void Awake()
    {
        _gyro = Input.gyro;
        _gyro.enabled = true;
        //cAndT = (CoinsAndTime)GameObject.FindGameObjectWithTag("CoinsAndTime");
    }

    private void Update()
    {
        _turnSensibility = _sensibilitySlider.value;

        if (!cAndT.paused && CheckFinish.finish.startRace)
        {
            float rotationRate = _gyro.rotationRate.z;

            if (Mathf.Abs(rotationRate) >= 0.05f)
            {
                _resetCount = 0;
                _currentRotation = Mathf.Clamp(_currentRotation + rotationRate * Time.deltaTime, -160, 160);
            }
            else
            {
                _resetCount += Time.deltaTime;
                if (_resetCount >= 0.6f && Mathf.Abs(_currentRotation) < 20)
                {
                    _currentRotation = 0;
                }
            }

            if (accelerating && _acceleration <= _speed)
            {
                _anim.SetBool("Accelerating", true);
                ChangeAcceleration(0.75f);
            }
            else if (breaking)
            {
                _anim.SetBool("Breaking", true);
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
                _anim.SetBool("Turbo", true);
                ChangeBoost(-Time.deltaTime * _boostDepletionRate);
                ChangeAcceleration(2.5f, 0, _speed * 1.5f);
            }
            else
            {
                _anim.SetBool("Accelerating", false);
                _anim.SetBool("Turbo", false);
                _anim.SetBool("Breaking", false);
                if (_acceleration > 0)
                {
                    ChangeAcceleration(-0.5f);
                }
                else if (_acceleration < 0)
                {
                    ChangeAcceleration(0.5f, _speed * -0.5f);
                    _currentRotation *= -1;
                }
            }

            var dir = transform.forward + transform.right * -_currentRotation * _turnSensibility;
            dir.Normalize();

            AddForce(dir);

            Move();
        }
    }

    public void ChangeBoost(float amount)
    {
        _boost = Mathf.Clamp(_boost + amount, 0, _maxBoost);

        _boostBar.fillAmount = Mathf.InverseLerp(0, _maxBoost, _boost);
    }
}
