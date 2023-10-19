using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using System;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI coinsT;
    public TextMeshProUGUI priceT;
    public TextMeshProUGUI recordT;
    public TextMeshProUGUI lastTimeT;
    public CustomJsonSaveSystem Json;
    public int actualCoins;
    public int actualEnergy;
    public Image Ienergy;
    public List<DriveController> actualCars = new List<DriveController>();
    public List<Sprite> posibleEnergys = new List<Sprite>();

    private void Awake()
    {
        
    }

    private void Start()
    {
        Json.LoadGame();
        recordT.text = "Best Time: " + Json.saveData._record.ToString();
        lastTimeT.text = "Last Time: " + Json.saveData._lastTime.ToString();
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
        if (actualEnergy == 0) Ienergy.sprite = posibleEnergys[0];
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
        Json.saveData._coins = 2;
        Json.saveData._record = default;
        Json.saveData._lastTime = default;
        Json.saveData.cars.Clear();
        Save();
    }

    public void Buy()
    {
        if(actualCoins > 0)
        {
            SelectCar();
        }
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

        Json.saveData._coins -= 1;
        //AGREGAR EL AUTO A LA LISTA
        //Json.saveData.cars.Add();
    }
}
