using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBoardManager : MonoBehaviour
{
    public Transform inputAndKeyboard;
    public Camera centerCam;

    private float posSpeed = 2.0f;
    private float rotSpeed = 2.0f;

    [SerializeField] private float yOffest = - 0.1f;
    [SerializeField] private float zOffest = 0.5f;

    private void Update()
    {
        Vector3 distance = inputAndKeyboard.position -  centerCam.transform.position;
        Quaternion rot = Quaternion.LookRotation(distance);

        Vector3 position = centerCam.transform.position + centerCam.transform.rotation * new Vector3 (0, yOffest, zOffest);

        // Lerp the position and rotation of the keyboard
        inputAndKeyboard.position = Vector3.Lerp(inputAndKeyboard.position, position, posSpeed * Time.deltaTime);
        inputAndKeyboard.rotation = Quaternion.Slerp(inputAndKeyboard.rotation, rot, rotSpeed * Time.deltaTime);

        // inputAndKeyboard.localScale = Vector3.one * 0.8f;
    }

}
