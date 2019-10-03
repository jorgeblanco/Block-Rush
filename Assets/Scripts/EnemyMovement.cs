using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private List<Waypoint> path;
    [SerializeField] private float dwellTime = 1f;

    public void SetPath(List<Waypoint> newPath)
    {
        path = newPath;
        StartCoroutine(EnemyPatrol());
    }

    private IEnumerator EnemyPatrol()
    {
        foreach (var waypoint in path)
        {
            transform.position = waypoint.transform.position;
            yield return new WaitForSeconds(dwellTime);
        }
    }
}
