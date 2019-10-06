using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private Transform towerHead;
    [SerializeField] private Transform enemyTarget;
    
    void Update()
    {
        LookAtEnemy();
    }

    private void LookAtEnemy()
    {
        towerHead.LookAt(enemyTarget);
    }
}
