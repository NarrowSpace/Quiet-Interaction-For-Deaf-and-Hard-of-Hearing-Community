using UnityEngine;
using UnityEngine.EventSystems;

public class RightButtonHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public ServoCommandReceiver servoReceiver;
    public string command = "/right";

    public void OnPointerDown(PointerEventData eventData)
    {
        servoReceiver.StartSendingCommand(command);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        servoReceiver.StopCurrentCoroutine();
    }
}
