using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{
    public enum PickupType
    {
        Coin,
        Boost
    }

    public PickupType type;
    [SerializeField] float _value;

    Renderer _renderer;
    Collider _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _renderer = GetComponent<Renderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            DriveController player = other.GetComponent<DriveController>();

            switch (type)
            {
                case PickupType.Coin:
                    player.cAndT.TakeCoin();
                    StartCoroutine(Reactivate());
                    break;
                case PickupType.Boost:
                    player.ChangeBoost(_value);
                    StartCoroutine(Reactivate());
                    break;
                default:
                    break;
            }
        }
    }

    IEnumerator Reactivate()
    {
        _collider.enabled = false;
        _renderer.enabled = false;

        yield return new WaitForSeconds(30f);

        _collider.enabled = true;
        _renderer.enabled = true;
    }
}
