using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TableLampHandler : MonoBehaviour
{
    [SerializeField] private GameObject _toolTip;
    [SerializeField] private Button On;
    [SerializeField] private Button Off;
    [SerializeField] private MQTT_Receiver mqtt;

    bool isToolVisible = false; 

    private void Start()
    {
        _toolTip.SetActive(false);

        // Add listener to the buttons
        On.onClick.AddListener(TurnOnSmartLamp);
        Off.onClick.AddListener(TurnOffSmartLamp);
    }

    // For Select Event 
    public void ShowToolTip()
    {
        isToolVisible = !isToolVisible;
        _toolTip.SetActive(isToolVisible);
    }

    private void TurnOnSmartLamp()
    {
        string message = "{\"state\": \"ON\"}";
        mqtt.Publish(0, message);
    }

    private void TurnOffSmartLamp()
    {
        string message = "{\"state\": \"OFF\"}";
        mqtt.Publish(0, message);
    }

}
