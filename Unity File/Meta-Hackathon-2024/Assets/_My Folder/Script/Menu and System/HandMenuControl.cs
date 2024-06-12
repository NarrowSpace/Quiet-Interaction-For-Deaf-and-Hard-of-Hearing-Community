using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMenuControl : MonoBehaviour
{
    // Follow the Camera Movement
    [SerializeField]
    private Transform _leftHandPos;
    [SerializeField]
    private Transform _handMenu;
    [SerializeField] private Camera _centerCam;

    [SerializeField]
    private float _posSpeed = 4.0f;
    [SerializeField]
    private float _rotSpeed = 4.0f;

    [SerializeField]
    private float _menuOffest = 0.1f;

    private void Update()
    {
        if (_handMenu != null)
        {
            HandMenuMovements(_menuOffest);
        }
    }

    private void HandMenuMovements(float y)
    {
        // Make the hand menu always face the camera
        Vector3 distance = _handMenu.position - _centerCam.transform.position; 
        Quaternion rotation = Quaternion.LookRotation(distance);
        _handMenu.rotation = Quaternion.Slerp(_handMenu.rotation, rotation, _rotSpeed * Time.deltaTime);

        // Make the hand menu follow the movment of left hand
        Vector3 yOffest = new Vector3(0, y, 0);
        Vector3 position = _leftHandPos.position + yOffest;
        _handMenu.position = Vector3.Lerp(_handMenu.position, position, _posSpeed * Time.deltaTime);

        // Learp the Position
        // _handMenu.transform.position = Vector3.Lerp(_handMenu.transform.position, position, _posSpeed * Time.deltaTime);

        // Scale the Canvas
        // _handMenu.transform.localScale = Vector3.one * 0.8f;
    }
}
