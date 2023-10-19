using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckFinish : MonoBehaviour
{
    public CoinsAndTime cAndT;
    [SerializeField] int _playerVueltas, _iaVueltas;
    public bool canPass;
    //[SerializeField] GameObject Finish;
    private void Start()
    {
        _playerVueltas = 0;
        _iaVueltas = 0;
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
            if (_playerVueltas >= 3)
            {
                cAndT.Win();
            }
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            //Finish.SetActive(true);
            _iaVueltas++;
            if (_iaVueltas >= 3)
            {
                cAndT.Lose();
            }
        }
    }
}
