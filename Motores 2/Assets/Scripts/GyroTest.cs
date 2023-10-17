using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroTest : MonoBehaviour
{
    Gyroscope _gyro;
    [SerializeField] Camera _camera;

    private void Start()
    {
        _gyro = Input.gyro;
        _gyro.enabled = true;
    }

    void Update()
    {
        _camera.transform.eulerAngles -= new Vector3(0, 0, _gyro.rotationRate.z)*Time.deltaTime*5;
    }
}
