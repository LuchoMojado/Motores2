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
    public Button BNormal;
    public Button BEpico;
    public DriveController EpicPreffab;
    public DriveController NormalPreffab;
    public DriveController ComonPreffab;

    private void Start()
    {
        Json.SaveGame();
        Json.LoadGame();
        recordT.text = "Best Time: " + Json.saveData._record.ToString();
        lastTimeT.text = "Last Time: " + Json.saveData._lastTime.ToString();
        Json.saveData.cars.Add(ComonPreffab);
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
        else
        {
            Bplay.GetComponent<Image>().raycastTarget = true;
            Bplay.GetComponent<Image>().color = Color.white;
        }
        if(actualEnergy <= 4)
        {
            restoreEnergy += Time.deltaTime;
            restTimeLeft.text = "Add Energy in: " + (int)restoreEnergy + "seg/ 5 mins";
            if (restoreEnergy >= 300)
            {
                Json.saveData._energy += 1;
                restoreEnergy = 0;
            }
        }
    }

    public void SubtractEnergy()
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
        Json.GainCoins(1);
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
            Json.saveData.cars.Add(EpicPreffab);
            HaveCars();
        }
        if (newCar >= 1 && newCar <= 3)
        {
            priceT.text = "Ganaste Un NORMAL";
            Json.saveData.cars.Add(NormalPreffab);
            HaveCars();
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

    public void ChooseYourCar(int presed)
    {
        if (presed == 0)
            Json.saveData.selectedCar = 0;
        if (presed == 1)
            Json.saveData.selectedCar = 1;
        if (presed == 2)
            Json.saveData.selectedCar = 2;
    }

    public void HaveCars()
    {
        foreach(var item in actualCars)
        {
            if (item.Class == "Normal")
                BNormal.gameObject.SetActive(true);
            if (item.Class == "Epic")
                BEpico.gameObject.SetActive(true);
        }
        
    }
    public void CheckTutorial()
    {
        if (!Json.saveData.madeTutorial)
        {
            change.NewScene("Tutorial");
        }
    }
}
