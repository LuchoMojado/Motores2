using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckFinish : MonoBehaviour
{
    public CoinsAndTime cAndT;
    int vueltas;
    [SerializeField] GameObject Finish;
    private void Start()
    {
        vueltas = 0;
        Finish.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Finish"))
        {
            vueltas++;
            print(vueltas);
            Finish.SetActive(false);
            if(vueltas >= 2)
            {
                cAndT.Win(this.gameObject.layer);
            }
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Finish On"))
        {
            Finish.SetActive(true);
        }
    }
}
