using System;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private int enemyHitPoints = 15;
    [SerializeField] private GameObject enemyExplosionFx;
    [SerializeField] private GameObject enemyDeathFx;
    [SerializeField] private GameObject enemyDamageFx;
    [SerializeField] private int damageToEnemy = 1;
    [SerializeField] private int damageToBase = 10;
    private Base _base;

    private void Start()
    {
        _base = FindObjectOfType<Base>();
    }

    private void OnParticleCollision(GameObject otherCollider)
    {
        DamageEnemy(damageToEnemy, otherCollider.transform);  //todo: get damage from colliding object
    }

    public void DamageEnemy(int damage, Transform damageTransform)
    {
        enemyHitPoints -= damage;
        if (enemyHitPoints <= 0)
        {
            // Kill the enemy if the HP goes below zero
            DestroyEnemy();
        }
        else
        {
            // Trigger damage effect
            Instantiate(enemyDamageFx, transform.position, Quaternion.Inverse(damageTransform.rotation));
        }
    }

    private void DestroyEnemy()
    {
        Instantiate(enemyDeathFx, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void ExplodeEnemy()
    {
        Instantiate(enemyExplosionFx, transform.position, Quaternion.identity);
        _base.DamageBase(damageToBase);
        Destroy(gameObject);
    }
}
