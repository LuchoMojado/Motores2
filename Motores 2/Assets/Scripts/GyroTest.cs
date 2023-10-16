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
    }

    void Update()
    {
        _camera.transform.rotation = new Quaternion(0, 0, _gyro.attitude.z, 0);
    }
}
