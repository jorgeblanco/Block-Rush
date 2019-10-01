using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[ExecuteAlways]
[SelectionBase]
public class CubeEditor : MonoBehaviour
{
    private TextMeshPro _text;
    [SerializeField] [Range(1f, 20f)] private float gridSize = 10f;

    void Awake()
    {
        _text = GetComponentInChildren<TextMeshPro>();
        if (_text == null)
        {
            Debug.Log("Cannot find text element");
        }
    }

    void Update()
    {
        // Calculate the snap position
        Vector3 snapPos = transform.position;
        snapPos.x = Mathf.RoundToInt(snapPos.x / gridSize) * gridSize;
        snapPos.z = Mathf.RoundToInt(snapPos.z / gridSize) * gridSize;
        snapPos.y = 0f;  // Always set Y to zero to match the "ground" plane

        // Set the transform to the snap position
        transform.position = snapPos;
        
        // Update the text label with the current snap position
        _text = GetComponentInChildren<TextMeshPro>();
        _text.SetText(snapPos.x / gridSize + "," + snapPos.z / gridSize);
    }
}
