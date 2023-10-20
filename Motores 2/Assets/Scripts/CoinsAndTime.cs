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
    public Image win;
    public Image lose;
    public Image pause;
    public Image uiGame;
    public TextMeshProUGUI WinRecordT;
    public TextMeshProUGUI WinTimeT;

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
        NewRecord();
        Json.SaveGame();
        win.gameObject.SetActive(true);
        WinRecordT.text = "Record: " + Json.saveData._record.ToString();
        WinTimeT.text = "Time: " + finishTime.ToString();
        uiGame.gameObject.SetActive(false);
    }
    public void Lose()
    {
        uiGame.gameObject.SetActive(false);
        lose.gameObject.SetActive(true);
        Debug.Log("Perdiste");
    }
    public void PauseS()
    {
        Time.timeScale = 0;
        uiGame.gameObject.SetActive(false);
        pause.gameObject.SetActive(true);
    }
    public void UnPauseS()
    {
        Time.timeScale = 1;
        uiGame.gameObject.SetActive(true);
        pause.gameObject.SetActive(false);
    }

    public void TakeCoin()
    {
        Json.saveData._coins += 1;
    }

    public void NewRecord()
    {
        if(finishTime > Json.saveData._record)
        {
            Json.saveData._record = finishTime;
            Debug.Log("NEW RECORD");
        }
        else
        {
            Debug.Log("Show new time");
            Json.saveData._lastTime = finishTime;
        }
    }
}
