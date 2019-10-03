using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] private Waypoint pathStart;
    [SerializeField] private Waypoint pathEnd;
    [SerializeField] private Material materialStart;
    [SerializeField] private Material materialEnd;
    [SerializeField] private Material materialExplore;
    private readonly Dictionary<Vector2Int, Waypoint> _grid = new Dictionary<Vector2Int, Waypoint>();

    public static class Direction
    {
        public static readonly Vector2Int Up = Vector2Int.up;
        public static readonly Vector2Int Right = Vector2Int.right;
        public static readonly Vector2Int Down = Vector2Int.down;
        public static readonly Vector2Int Left = Vector2Int.left;
        public static readonly Vector2Int[] Directions = new[] {Up, Right, Down, Left};
    }
    
    void Start()
    {
        LoadBlocks();
        ColorBlocks();
        ExploreNeighbors(pathStart);
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

    public void ColorBlocks()
    {
        // Color blocks
        pathStart.SetTopMaterial(materialStart);
        pathEnd.SetTopMaterial(materialEnd);
    }

    private void ExploreNeighbors(Waypoint waypoint)
    {
        var startPos = waypoint.GetGridPosition();
        foreach (var direction in Direction.Directions)
        {
            var explorePosition = startPos + direction;
            if (_grid.ContainsKey(explorePosition))
            {
                Debug.Log("block: " + explorePosition + " in " + _grid[explorePosition]);
                _grid[explorePosition].SetTopMaterial(materialExplore);
            }
        }
    }
}
