using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ServoCommandReceiver : MonoBehaviour
{

    [Header("Servo Controller Port")]
    public string URL = "http://10.0.0.83:5500";
    private Coroutine sendCommandCoroutine = null;

    public void StartSendingCommand(string command)
    {
        if (sendCommandCoroutine != null ) StopCoroutine(sendCommandCoroutine);
        sendCommandCoroutine = StartCoroutine(SendCommand(command));
    }

    public void StopCurrentCoroutine()
    {
        if(sendCommandCoroutine != null)
        {
            StopCoroutine(sendCommandCoroutine);
            sendCommandCoroutine = null;
        }
    }

    private IEnumerator SendCommand(string command)
    {
        UnityWebRequest request = UnityWebRequest.Get(URL + command);
        yield return request.SendWebRequest();
    }
}
