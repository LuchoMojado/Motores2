using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using System;
using TMPro;
using UnityEngine.Audio;

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
    public Button losePlayAgain;
    public TextMeshProUGUI WinRecordT;
    public TextMeshProUGUI WinTimeT;
    public bool paused = false;
    public List<DriveController> sceneCars = new List<DriveController>();
    [SerializeField] Slider _audioSlider;
    [SerializeField] AudioMixer _myAM;
    public int carIndex;

    private void Start()
    {
        Json.LoadGame();
        Time.timeScale = 1;
        if(Json.saveData.selectedCar == 0)
        {
            //Prender primer auto
            sceneCars[0].gameObject.SetActive(true);
            carIndex = 0;
        }
        else if (Json.saveData.selectedCar == 1)
        {
            //Prender segundo auto
            sceneCars[1].gameObject.SetActive(true);
            carIndex = 1;
        }
        else if (Json.saveData.selectedCar == 2)
        {
            //Prender tercero auto
            sceneCars[2].gameObject.SetActive(true);
            carIndex = 2;
        }
    }

    void Update()
    {
        _myAM.SetFloat("Master", _audioSlider.value);

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
        ReTry();
        Debug.Log("Perdiste");
    }

    public void ReTry()
    {
        if(Json.saveData._energy > 0)
        {
            Json.saveData._energy -= 1;
            losePlayAgain.GetComponent<Image>().raycastTarget = true;
            losePlayAgain.GetComponent<Image>().color = Color.white;
        }
        else
        {

            losePlayAgain.GetComponent<Image>().raycastTarget = false;
            losePlayAgain.GetComponent<Image>().color = Color.red;
        }
    }
    public void PauseS()
    {
        paused = true;
        Time.timeScale = 0;
        uiGame.gameObject.SetActive(false);
        pause.gameObject.SetActive(true);
    }
    public void UnPauseS()
    {
        paused = false;
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
        if (finishTime < Json.saveData._record)
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
