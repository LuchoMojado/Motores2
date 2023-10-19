using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using System;
using TMPro;

public class CoinsAndTime : MonoBehaviour
{
    public TextMeshProUGUI coinsT;
    public TextMeshProUGUI timeT;
    public CustomJsonSaveSystem Json;
    float actualTime;
    float finishTime;

    // Update is called once per frame
    void Update()
    {
        actualTime += Time.deltaTime;
        int firstNumber = Mathf.FloorToInt(actualTime);
        int decimales = Mathf.FloorToInt((actualTime - firstNumber) * 100);
        string newTime = firstNumber.ToString("D1") + "." + decimales.ToString("D2");
        timeT.text = newTime;

        coinsT.text = Json.saveData._coins.ToString();
        
    }

    public void Win()
    {
        finishTime = actualTime;
    }

    public void NewRecord()
    {
        if(finishTime > Json.saveData._record)
        {
            Debug.Log("NEW RECORD");
        }
        else
        {
            Debug.Log("Show new time");
            Json.saveData._lastTime = finishTime;
        }
    }
}
