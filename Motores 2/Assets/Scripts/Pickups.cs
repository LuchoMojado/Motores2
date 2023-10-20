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
        gameObject.SetActive(false);

        yield return new WaitForSeconds(30f);

        gameObject.SetActive(true);
    }
}
