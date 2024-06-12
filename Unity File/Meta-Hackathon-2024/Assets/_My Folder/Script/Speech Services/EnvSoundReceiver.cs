using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class EnvSoundReceiver : MonoBehaviour
{
    [SerializeField] private string URL = "http://10.0.0.193:6600/env_sound";
    [SerializeField] private TextMeshProUGUI predictionText;

    public UDPReceive udpReceive;

    public void StartGetPredictionCoroutine()
    {
        StartCoroutine(GetPrediction());
        Debug.Log("Environmental Sound Detection Coroutine started");
    }

    private IEnumerator GetPrediction()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(URL))
        {
            // Send the request
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log("Error: " + webRequest.error);
            }
            else
            {
                string jsonResponse = webRequest.downloadHandler.text;
                // Debug.Log("Raspi Sound Detection: " + jsonResponse);
                predictionText.text = FormattedText(jsonResponse);
            }

        }
    }

    private string FormattedText(string receivedText)
    {
        // Remove curly braces
        receivedText = receivedText.Replace("{", "").Replace("}", "");

        // Split by comma
        string[] predictions = receivedText.Split(',');

        List<string> formattedPredictions = new List<string>();

        foreach (var prediction in predictions)
        {
            // Split by colon and remove double quotes
            string[] keyValue = prediction.Split(':');

            if (keyValue.Length == 2)
            {
                string key = keyValue[0].Replace("\"", "").Trim();
                string value = keyValue[1].Replace("\"", "").Trim();

                formattedPredictions.Add(key + ": " + value);
            }
        }

        // Join the formatted predictions with newline
        string result = string.Join("\n", formattedPredictions);

        Debug.Log("Formatted data: " + result);

        return result;
    }


}
