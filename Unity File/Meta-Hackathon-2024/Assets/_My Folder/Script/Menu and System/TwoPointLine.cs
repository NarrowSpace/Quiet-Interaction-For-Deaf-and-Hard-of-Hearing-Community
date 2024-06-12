using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TwoPointLine : MonoBehaviour
{
    public Transform _pointA;
    public Transform _pointB;
    private LineRenderer _lineRenderer;

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();  
    }
    private void Update()
    {
        _lineRenderer.positionCount = 2;
        _lineRenderer.SetPosition(0, _pointA.position);
        _lineRenderer.SetPosition(1, _pointB.position);
    }
}
