using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DriveButtons : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] DriveController _car;

    public enum ButtonType
    {
        Accelerate,
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
            case ButtonType.Break:
                _car.breaking = false;
                break;
            default:
                break;
        }
    }
}
