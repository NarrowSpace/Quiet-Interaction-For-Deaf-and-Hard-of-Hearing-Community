using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurtainEventHandler : MonoBehaviour
{
    [SerializeField] private GameObject _toolTip;
    [SerializeField] private CurtainAnimation _curtainAnim;
    [SerializeField] private Slider slider;
    [SerializeField] private MQTT_Receiver mqtt;

    [SerializeField] private Button On;
    [SerializeField] private Button Off;
    [SerializeField] private Button stop;

    bool isToolVisible = false;

    private void Start()
    {
        _toolTip.SetActive(false);
        slider.onValueChanged.AddListener(delegate {ChangeCurtainPos(); });

        // Add listener to the buttons
        On.onClick.AddListener(TurnOnBlind);
        Off.onClick.AddListener(TurnOffBlind);
        stop.onClick.AddListener(StopBlind);
    }
    public void ShowToolTip()
    {
        isToolVisible = !isToolVisible;
        _toolTip.SetActive(isToolVisible);
    }

    private void StopBlind()
    {
        string message = "{\"state\": \"STOP\"}";
        mqtt.Publish(1, message);
    }

    private void TurnOnBlind()
    {
        string message = "{\"state\": \"OPEN\"}";
        mqtt.Publish(1, message);
        slider.value = 100;
        _curtainAnim.BlindRotation(100);
    }
    private void TurnOffBlind()
    {
        string message = "{\"state\": \"CLOSE\"}";
        mqtt.Publish(1, message);
        slider.value = 0;
        _curtainAnim.BlindRotation(0);
    }

    private void ChangeCurtainPos()
    {
        _curtainAnim.BlindRotation(slider.value);
        string message = "{\"position\": " + slider.value + "}";
        mqtt.Publish(1, message);
    }
}
