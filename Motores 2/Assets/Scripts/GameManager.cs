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
    public CustomJsonSaveSystem Json;
    public int actualCoins;
    public List<DriveController> actualCars = new List<DriveController>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        actualCars = Json.saveData.cars;
        actualCoins = Json.saveData._coins;
        coinsT.text = "You Have: " + actualCoins.ToString();
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
