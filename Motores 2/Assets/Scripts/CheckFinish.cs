using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CheckFinish : MonoBehaviour
{
    public static CheckFinish finish;
    public CoinsAndTime cAndT;
    [SerializeField] int _playerVueltas, _iaVueltas;
    public bool canPass;
    public TMP_Text countdown;
    //[SerializeField] GameObject Finish;
    public bool startRace;
    void Awake()
    {
        if (finish == null) finish = this;
        else Destroy(gameObject);
        //_countdown = GameObject.FindGameObjectWithTag("xd").GetComponent<TMP_Text>();
    }
    private void Start()
    {
        _playerVueltas = 0;
        _iaVueltas = -1;
        canPass = false;
        StartCoroutine(Timer());
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
                gameObject.SetActive(false);
                cAndT.Win();
            }
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            //Finish.SetActive(true);
            _iaVueltas++;
            if (_iaVueltas >= 3)
            {
                gameObject.SetActive(false);
                cAndT.Lose();
            }
        }
    }
    IEnumerator Timer()
    {
        countdown.enabled = true;
        startRace = false;
        countdown.text = "3";
        yield return new WaitForSeconds(1);
        countdown.text = "2";
        yield return new WaitForSeconds(1);
        countdown.text = "1";
        yield return new WaitForSeconds(1);
        countdown.enabled = false;
        startRace = true;
    }
}
