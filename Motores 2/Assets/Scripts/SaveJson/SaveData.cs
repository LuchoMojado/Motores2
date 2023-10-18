using System;
using System.Collections.Generic;
using UnityEngine;


//Es una variable que se le pueden guardar datos toda la clase
[Serializable]
public class SaveData
{
    public int _energy;
    public int _coins = 2;
    public float _record;
    public float _lastTime;
    public List<string> cars = new List<string>();
}
