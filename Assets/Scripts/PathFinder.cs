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
    [SerializeField] private Material materialDefault;
    [SerializeField] private Material materialExplore;
    private readonly Dictionary<Vector2Int, Waypoint> _grid = new Dictionary<Vector2Int, Waypoint>();
    private readonly Dictionary<Vector2Int, Waypoint> _exploredGrid = new Dictionary<Vector2Int, Waypoint>();
    private readonly Queue<Waypoint> _queue = new Queue<Waypoint>();
    private State _state = State.PathFinding;
    private readonly List<Waypoint> _path = new List<Waypoint>();
    private EnemyMovement _enemyMovement;
    [SerializeField] private float delay = 0f;

    enum State
    {
        Idle,
        PathFinding,
        PathFound,
        PathNotFound
    }

    public static class Direction
    {
        public static readonly Vector2Int Up = Vector2Int.up;
        public static readonly Vector2Int Right = Vector2Int.right;
        public static readonly Vector2Int Down = Vector2Int.down;
        public static readonly Vector2Int Left = Vector2Int.left;
        public static readonly Vector2Int[] Directions = {Up, Right, Down, Left};
    }
    
    void Start()
    {
        LoadBlocks();
        ColorBlocks();
        StartCoroutine(PathFind());
        _enemyMovement = FindObjectOfType<EnemyMovement>();
    }

    private void Update()
    {
        if (_state == State.PathFound)
        {
            HandlePath();
            _enemyMovement.SetPath(_path);
        }
    }

    private void LoadBlocks()
    {
        var waypoints = GetComponentsInChildren<Waypoint>();
        foreach (var waypoint in waypoints)
        {
            var gridPosition = waypoint.GetGridPosition();
            if (_grid.ContainsKey(gridPosition))
            {
                throw new ApplicationException("Overlapping block on: " + gridPosition);
            }
            
            _grid.Add(waypoint.GetGridPosition(), waypoint); 
        }
    }

    public void ColorBlocks()
    {
        // Color blocks
        var waypoints = FindObjectsOfType <Waypoint>();
        foreach (var waypoint in waypoints)
        {
            waypoint.SetTopMaterial(materialDefault);
        }
        pathStart.SetTopMaterial(materialStart);
        pathEnd.SetTopMaterial(materialEnd);
    }

    private void ExploreNeighbors(Waypoint waypoint)
    {
        var startPos = waypoint.GetGridPosition();
        foreach (var direction in Direction.Directions)
        {
            var explorePosition = startPos + direction;
            
            // Skip if the neighbor is already explored or non-existent
            if (!_grid.ContainsKey(explorePosition) || _exploredGrid.ContainsKey(explorePosition)) continue;
            
            var neighbor = _grid[explorePosition];
            neighbor.SetTopMaterial(materialExplore);
            _queue.Enqueue(neighbor);
            _exploredGrid[explorePosition] = waypoint;
        }
    }

    private IEnumerator PathFind()
    {
        _queue.Enqueue(pathStart);
        _exploredGrid[pathStart.GetGridPosition()] = null;

        while (_queue.Count > 0)
        {
            yield return new WaitForSeconds(delay);
            var nextWaypoint = _queue.Dequeue();
            if (nextWaypoint == pathEnd)
            {
                _state = State.PathFound;
                yield break;
            }
            ExploreNeighbors(nextWaypoint);
        }
        Debug.Log("Path not found :(");
        _state = State.PathNotFound;
    }

    private void HandlePath()
    {
        var nextWaypoint = pathEnd;
        _path.Add(pathEnd);
        while (nextWaypoint != pathStart && _exploredGrid[nextWaypoint.GetGridPosition()] != null)
        {
            nextWaypoint = _exploredGrid[nextWaypoint.GetGridPosition()];
            _path.Add(nextWaypoint);
        }
        _path.Reverse();
        _state = State.Idle;
    }
}
