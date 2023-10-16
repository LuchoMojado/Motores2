using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroTest : MonoBehaviour
{
    Gyroscope _gyro;

    private void Start()
    {
        _gyro = Input.gyro;
    }

    void Update()
    {
        Debug.Log(_gyro.attitude.z);
    }
}
