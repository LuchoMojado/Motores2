using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChekFinishTuto : MonoBehaviour
{
    [SerializeField] int _playerVueltas;
    public bool canPass;
    public CustomJsonSaveSystem Json;
    public ChangeScene change;
    //[SerializeField] GameObject Finish;
    private void Start()
    {
        _playerVueltas = 0;
        canPass = false;
        //Finish.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Car") && canPass)
        {
            _playerVueltas++;
            canPass = false;
            //print(_playerVueltas);
            //Finish.SetActive(false);
            if (_playerVueltas >= 1)
            {
                //cAndT.Win();
                Json.saveData.madeTutorial = true;
                Json.SaveGame();
                change.NewScene("Menu 1");
            }
        }
    }
}
