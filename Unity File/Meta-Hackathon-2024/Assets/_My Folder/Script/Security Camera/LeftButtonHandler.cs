using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LeftButtonHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public ServoCommandReceiver servoReceiver;
    public string command = "/left";

    public void OnPointerDown(PointerEventData eventData)
    {
        servoReceiver.StartSendingCommand(command);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        servoReceiver.StopCurrentCoroutine();
    }
}
