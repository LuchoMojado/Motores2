using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DriveButtons : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    DriveController _car;
    [SerializeField] CoinsAndTime cAndT;

    private void Start()
    {
        _car = cAndT.sceneCars[cAndT.carIndex];
    }

    public enum ButtonType
    {
        Accelerate,
        Boost,
        Break
    }

    [SerializeField] ButtonType _type;

    public void OnPointerDown(PointerEventData eventData)
    {
        switch (_type)
        {
            case ButtonType.Accelerate:
                _car.accelerating = true;
                break;
            case ButtonType.Boost:
                _car.boosting = true;
                break;
            case ButtonType.Break:
                _car.breaking = true;
                break;
            default:
                break;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        switch (_type)
        {
            case ButtonType.Accelerate:
                _car.accelerating = false;
                break;
            case ButtonType.Boost:
                _car.boosting = false;
                break;
            case ButtonType.Break:
                _car.breaking = false;
                break;
            default:
                break;
        }
    }
}
