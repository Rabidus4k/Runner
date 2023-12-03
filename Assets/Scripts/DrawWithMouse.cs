using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DrawWithMouse : MonoBehaviour
{
    [SerializeField] private float _minDistance = 0.1f;
    [SerializeField] private LayerMask _layerMask;

    public event Action<Vector3[]> OnDrawPath;
    private Vector3 _previousPos;
    private LineRenderer _line;


    private void Start()
    {
        _line = GetComponent<LineRenderer>();
        ResetLine();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ResetLine();
        }

        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5));
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit, 5, _layerMask))
            {
                DrawPoint(hit.point);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            FinishLine();
        }
    }

    private void FinishLine()
    {
        Vector3[] newPos = new Vector3[_line.positionCount];
        _line.GetPositions(newPos);

        OnDrawPath?.Invoke(newPos);
    }

    private void ResetLine()
    {
        _line.positionCount = 1;
        _previousPos = default;
    }

    private void DrawPoint(Vector3 pos)
    {
        pos.z = 0;

        if (Vector3.Distance(pos, _previousPos) > _minDistance)
        {
            if (_previousPos == default)
            {
                _line.SetPosition(0, pos);
            }
            else
            {
                _line.positionCount++;
                _line.SetPosition(_line.positionCount - 1, pos);
            }
            _previousPos = pos;
        }
    }
}
