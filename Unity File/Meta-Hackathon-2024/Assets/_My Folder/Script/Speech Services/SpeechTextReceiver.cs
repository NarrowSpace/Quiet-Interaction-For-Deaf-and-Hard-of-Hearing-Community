using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpeechTextReceiver : MonoBehaviour
{
    [SerializeField] private UDPReceive udpReceive;
    [SerializeField] private TextMeshProUGUI receivedText;

    [Header("Menu Position")]
    [SerializeField] private Camera centerCam;
    [SerializeField] private GameObject liveCaptionCanvas;
    [SerializeField] private float yOffest = -0.1f;
    [SerializeField] private float zOffest = 0.45f;
    private float posSpeed = 2.0f;
    private float rotSpeed = 2.0f;

    private void Start()
    {
        receivedText.text = "Start Detecting...";
    }

    private void Update()
    {
        receivedText.text = udpReceive.data;
        UpdateCaptionPosition();
    }

    private void UpdateCaptionPosition()
    {
        Vector3 distance = liveCaptionCanvas.transform.position - centerCam.transform.position;
        Quaternion rot = Quaternion.LookRotation(distance);

        Vector3 position = centerCam.transform.position + centerCam.transform.rotation * new Vector3(0, yOffest, zOffest);

        // Lerp the position and rotation of the canvas
        liveCaptionCanvas.transform.position = Vector3.Lerp(liveCaptionCanvas.transform.position, position, posSpeed * Time.deltaTime);
        liveCaptionCanvas.transform.rotation = Quaternion.Slerp(liveCaptionCanvas.transform.rotation, rot, rotSpeed * Time.deltaTime);
    }
}
