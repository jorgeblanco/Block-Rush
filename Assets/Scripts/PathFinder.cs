﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] private Waypoint pathStart;
    [SerializeField] private Waypoint pathEnd;
    [SerializeField] private Color colorStart;
    [SerializeField] private Color colorEnd;
    private readonly Dictionary<Vector2Int, Waypoint> _grid = new Dictionary<Vector2Int, Waypoint>();
    
    void Start()
    {
        LoadBlocks();
    }

    private void LoadBlocks()
    {
        var waypoints = FindObjectsOfType<Waypoint>();
        foreach (var waypoint in waypoints)
        {
            var gridPosition = waypoint.GetGridPosition();
            if (_grid.ContainsKey(gridPosition))
            {
                throw new ApplicationException("Overlapping block on: " + gridPosition);
            }
            
            _grid.Add(waypoint.GetGridPosition(), waypoint); 
        }
        Debug.Log("Loaded " + _grid.Count + " blocks");
        
        ColorBlocks();
    }

    public void ColorBlocks()
    {
        // Color blocks
        pathStart.SetTopColor(colorStart);
        pathEnd.SetTopColor(colorEnd);
    }
}
