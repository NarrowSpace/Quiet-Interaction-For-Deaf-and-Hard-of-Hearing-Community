using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomPositionManager : MonoBehaviour
{
    public Transform roomModel;
    public Camera _centerCam;

    private void Start()
    {
        RoomModelPlacement();
    }

    private void RoomModelPlacement()
    {
        Vector3 distance = roomModel.position - _centerCam.transform.position;
        Quaternion rotation = Quaternion.LookRotation(distance);

        Vector3 roomPos = _centerCam.transform.position + _centerCam.transform.forward * 0.7f;
        roomPos.y -= 0.2f;

        roomModel.position = roomPos;
    }
}
