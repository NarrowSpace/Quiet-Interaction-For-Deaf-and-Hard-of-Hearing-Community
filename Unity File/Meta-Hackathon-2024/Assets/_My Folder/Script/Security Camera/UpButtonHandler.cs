using UnityEngine;
using UnityEngine.EventSystems;

public class UpButtonHandler : MonoBehaviour, IPointerDownHandler , IPointerUpHandler
{
    public ServoCommandReceiver servoReceiver;
    public string command = "/up";

    public void OnPointerDown(PointerEventData eventData)
    {
        servoReceiver.StartSendingCommand(command);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        servoReceiver.StopCurrentCoroutine();
    }

}
