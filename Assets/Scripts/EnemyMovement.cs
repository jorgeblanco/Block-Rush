using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private List<Waypoint> path;
    [SerializeField] private float dwellTime = 1f;

    private void Start()
    {
        StartCoroutine(GetPath());
    }

    private IEnumerator GetPath()
    {
        PathFinder pathFinder = FindObjectOfType<PathFinder>();
        while (true)
        {
            path = pathFinder.GetPath();
            if (path == null)
            {
                yield return new WaitForSeconds(0);
            }
            else
            {
                StartCoroutine(EnemyPatrol());
                yield break;
            }
        }
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
