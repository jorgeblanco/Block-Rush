using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
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
    }
}
