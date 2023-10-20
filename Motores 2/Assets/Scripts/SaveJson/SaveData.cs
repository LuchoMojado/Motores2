using System;
using System.Collections.Generic;
using UnityEngine;


//Es una variable que se le pueden guardar datos toda la clase
[Serializable]
public class SaveData
{
    public int _energy;
    public int _coins = 60;
    public float _record = 0;
    public float _lastTime = 0;
    public List<DriveController> cars = new List<DriveController>();
    public bool madeTutorial = false;
    public float timeToEnergy;
}
