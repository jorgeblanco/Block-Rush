using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private List<Waypoint> path;
    [SerializeField] private float dwellTime = 1f;
    [SerializeField] private int enemyHitPoints = 3;
    [SerializeField] private GameObject enemyExplosion;
    private int _damage = 1;

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

    private void OnParticleCollision(GameObject other)
    {
        DamageEnemy(_damage);  //todo: get damage from colliding object
    }

    public void DamageEnemy(int damage)
    {
        enemyHitPoints -= damage;
        if (enemyHitPoints <= 0)
        {
            DestroyEnemy();
        }
    }

    private void DestroyEnemy()
    {
        Instantiate(enemyExplosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
