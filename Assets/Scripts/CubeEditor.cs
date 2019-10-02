using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[ExecuteAlways]
[SelectionBase]
[RequireComponent(typeof(Waypoint))]
public class CubeEditor : MonoBehaviour
{
    private TextMeshPro _text;
    private Vector2Int _gridPosition;
    private Waypoint _waypoint;
    private int _gridSize;

    void Awake()
    {
        // TODO: This doesn't work for caching the TMP object
        _text = GetComponentInChildren<TextMeshPro>();
        if (_text == null)
        {
            Debug.Log("Cannot find text element");
        }

    }

    void Update()
    {
        // Setting up the variables
        _waypoint = GetComponent<Waypoint>();
        _gridSize = _waypoint.GetGridSize();
        _gridPosition = _waypoint.GetGridPosition();
        _text = GetComponentInChildren<TextMeshPro>();
        
        SnapToGrid();
        UpdateLabel();
    }

    private void UpdateLabel()
    {
        // Update the text label with the current snap position
        string label = _gridPosition.x + "," + _gridPosition.y;
        _text.SetText(label);
        gameObject.name = label;
    }

    private void SnapToGrid()
    {
        // Set the transform to the snap position
        transform.position = new Vector3(_gridPosition.x * _gridSize, 0f, _gridPosition.y * _gridSize);
    }
}
