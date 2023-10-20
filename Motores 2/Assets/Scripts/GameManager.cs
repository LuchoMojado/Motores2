using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using System;
using TMPro;

public class GameManager : MonoBehaviour
{
    public ChangeScene change;
    public TextMeshProUGUI coinsT;
    public TextMeshProUGUI priceT;
    public TextMeshProUGUI recordT;
    public TextMeshProUGUI lastTimeT;
    public TextMeshProUGUI restTimeLeft;
    public CustomJsonSaveSystem Json;
    public int actualCoins;
    public int actualEnergy;
    public Image Ienergy;
    public List<DriveController> actualCars = new List<DriveController>();
    public List<Sprite> posibleEnergys = new List<Sprite>();
    public Button Bplay;
    float restoreEnergy = 0;

    private void Start()
    {
        Json.LoadGame();
        recordT.text = "Best Time: " + Json.saveData._record.ToString();
        lastTimeT.text = "Last Time: " + Json.saveData._lastTime.ToString();
        if(Json.saveData.madeTutorial == false)
        {
            change.NewScene("Tutorial");
        }
    }

    // Update is called once per frame
    void Update()
    {
        actualCars = Json.saveData.cars;
        actualCoins = Json.saveData._coins;
        actualEnergy = Json.saveData._energy;
        coinsT.text = "You Have: " + actualCoins.ToString();
        if (actualEnergy == 5) Ienergy.sprite = posibleEnergys[5];
        if (actualEnergy == 4) Ienergy.sprite = posibleEnergys[4];
        if (actualEnergy == 3) Ienergy.sprite = posibleEnergys[3];
        if (actualEnergy == 2) Ienergy.sprite = posibleEnergys[2];
        if (actualEnergy == 1) Ienergy.sprite = posibleEnergys[1];
        if (actualEnergy == 0)
        {
            Ienergy.sprite = posibleEnergys[0];
            Bplay.GetComponent<Image>().raycastTarget = false;
            Bplay.GetComponent<Image>().color = Color.red;
        }
        if(actualEnergy <= 4)
        {
            restoreEnergy += Time.deltaTime;
            restTimeLeft.text = "Add Energy in: " + (int)restoreEnergy + "s/ 5 mins";
            if (restoreEnergy >= 300)
            {
                Json.saveData._energy += 1;
                restoreEnergy = 0;
                restTimeLeft.text = "Add Energy in: 0s/ 5 mins";
            }
        }
    }

    public void RestEnergy()
    {
        Json.saveData._energy -= 1;
    }

    public void Save()
    {
        Json.SaveGame();
    }

    public void DeleteGame()
    {
        Json.saveData._energy = 5;
        Json.saveData._coins = 60;
        Json.saveData._record = default;
        Json.saveData._lastTime = default;
        Json.saveData.cars.Clear();
        Json.saveData.madeTutorial = false;
        Save();
    }

    public void Buy()
    {
        if(actualCoins >= 30)
        {
            SelectCar();
        }
    }

    public void GainCoins()
    {
        Json.GainCoinsAndEnergy();
    }

    public void SelectCar()
    {
        float newCar;
        //HACER QUE SE HAGA MIENTRAS QUE NO TENGA EL AUTO
        /*
        do
        {
            newCar = Random.Range(0, 10);
        }while(newCar == )*/
        newCar = Random.Range(0, 10);
        if (newCar == 0)
        {
            priceT.text = "GANASTE UN EPICO";
            //Hacer varios prefabs de autos y cuando seleccione este ponerle el valor correspondiente
        }
        if (newCar >= 1 && newCar <= 3)
        {
            priceT.text = "Ganaste Un NORMAL";
            //Hacer varios prefabs de autos y cuando seleccione este ponerle el valor correspondiente
        }
        if (newCar >= 4 && newCar <= 10)
        {
            priceT.text = "Ganaste Un Comun";
            //Hacer varios prefabs de autos y cuando seleccione este ponerle el valor correspondiente
        }

        Json.saveData._coins -= 30;
        //AGREGAR EL AUTO A LA LISTA
        //Json.saveData.cars.Add();
    }
}
