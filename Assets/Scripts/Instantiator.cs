using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiator : MonoBehaviour
{
    [SerializeField] private Tower selectedTower;
    [SerializeField] private int maxNoOfTowers = 5;
    private Queue<Waypoint> _occupiedBlocks;
    private Queue<Tower> _towers;

    private void Start()
    {
        _occupiedBlocks = new Queue<Waypoint>(maxNoOfTowers);
        _towers = new Queue<Tower>(maxNoOfTowers);
    }

    public void PlaceSelectedAt(Waypoint waypoint)
    {
        if (_occupiedBlocks.Contains(waypoint)) return;
        
        Tower tower;
        if (_towers.Count < maxNoOfTowers)
        {
            tower = Instantiate(selectedTower, transform);
        }
        else
        {
            tower = _towers.Dequeue();
            _occupiedBlocks.Dequeue();
        }
        tower.transform.SetPositionAndRotation(waypoint.transform.position, Quaternion.identity);
        _towers.Enqueue(tower);
        _occupiedBlocks.Enqueue(waypoint);
    }
}
