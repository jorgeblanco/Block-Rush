using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private Transform towerHead;
    [SerializeField] private float towerRange = 40f;
    private Transform _enemyTarget;
    private bool _isTargeting;
    private ParticleSystem _bullets;

    private void Start()
    {
        _bullets = GetComponentInChildren<ParticleSystem>();
        StartCoroutine(GetClosestEnemy());
    }

    void Update()
    {
        if (_isTargeting)
        {
            LookAtEnemy();
            Shoot(true);
        }
        else
        {
            Shoot(false);
        }
    }

    private void LookAtEnemy()
    {
        towerHead.LookAt(_enemyTarget);
    }

    private IEnumerator GetClosestEnemy()
    {
        while (true)
        {
            var enemies = FindObjectsOfType<Enemy>();
            float minDistance = towerRange;
            _isTargeting = false;
            foreach (var enemy in enemies)
            {
                var distance = Vector3.Distance(enemy.transform.position, transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    _enemyTarget = enemy.transform;
                    _isTargeting = true;
                }
            }
            // wait a second before trying to acquire new targets
            yield return new WaitForSeconds(1);
        }
    }

    private void Shoot(bool isShooting)
    {
        var emissionModule = _bullets.emission;
        emissionModule.enabled = isShooting;
    }
}
