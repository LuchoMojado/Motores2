using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckFinish : MonoBehaviour
{
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
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Finish On"))
        {
            Finish.SetActive(true);
        }
    }
}
