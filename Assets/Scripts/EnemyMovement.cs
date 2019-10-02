using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private List<Waypoint> path;
    [SerializeField] private float dwellTime = 1f;
    
    void Start()
    {
        StartCoroutine(EnemyPatrol());
    }

    private IEnumerator EnemyPatrol()
    {
        Debug.Log("Starting patrol");
        foreach (var waypoint in path)
        {
            transform.position = waypoint.transform.position;
        Debug.Log("Visiting block: " + waypoint.name);
            yield return new WaitForSeconds(dwellTime);
        }
        Debug.Log("Ending patrol");
    }
}
