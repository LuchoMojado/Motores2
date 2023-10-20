using System.Collections;
using System.IO;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CustomJsonSaveSystem : MonoBehaviour
{
    public SaveData saveData = new SaveData();
    string path;

    private void Awake()
    {
        //string customDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments).Replace("\\","/") + "/" + Application.companyName + "/" + Application.productName;

        //if (!Directory.Exists(customDirectory)) Directory.CreateDirectory(customDirectory);

        path = Application.persistentDataPath + "/Iceberg.Incoming";

        //path = customDirectory + "/Iceberg.Incoming";

        //Debug.Log(path);
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.S)) SaveGame();
        //if (Input.GetKeyDown(KeyCode.L)) LoadGame();
        if (Input.GetKeyDown(KeyCode.J)) GainCoinsAndEnergy();
    }

    public void SaveGame()
    {
        //string json = JsonUtility.ToJson(saveData);
        //
        //Debug.Log(json);

        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(path, json);

        //Debug.Log(json);
    }

    public void LoadGame()
    {
        string json = File.ReadAllText(path);
        JsonUtility.FromJsonOverwrite(json, saveData);
    }

    public void GainCoinsAndEnergy()
    {
        saveData._coins = 90;
        saveData._energy = 5;
    }
}