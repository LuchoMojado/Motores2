using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoChekCanPass : MonoBehaviour
{
    [SerializeField] ChekFinishTuto Finish;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Car"))
        {
            Finish.canPass = true;
        }
    }
}
