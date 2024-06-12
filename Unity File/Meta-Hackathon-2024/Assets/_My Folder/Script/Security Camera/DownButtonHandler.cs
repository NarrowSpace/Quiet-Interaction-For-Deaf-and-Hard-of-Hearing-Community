using UnityEngine;
using UnityEngine.EventSystems;

public class DownButtonHandler : MonoBehaviour, IPointerDownHandler , IPointerUpHandler
{
    public ServoCommandReceiver servoReceiver;
    public string command = "/down";

    public void OnPointerDown(PointerEventData eventData)
    {
        servoReceiver.StartSendingCommand(command);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        servoReceiver.StopCurrentCoroutine();
    }

}
