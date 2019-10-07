using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiator : MonoBehaviour
{
    [SerializeField] private Tower selectedTower;
    private readonly List<Waypoint> _occupiedBlocks = new List<Waypoint>();
    
    public void PlaceSelectedAt(Waypoint waypoint)
    {
        if (!_occupiedBlocks.Contains(waypoint))
        {
            Instantiate(selectedTower, waypoint.transform.position, Quaternion.identity, transform);
            _occupiedBlocks.Add(waypoint);
        }
    }
}
