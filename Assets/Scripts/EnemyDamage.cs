using System;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [Header("Particles")]
    [SerializeField] private GameObject enemyExplosionFx;
    [SerializeField] private GameObject enemyDeathFx;
    [SerializeField] private GameObject enemyDamageFx;
    [Header("Configuration")]
    [SerializeField] private int enemyHitPoints = 15;
    [SerializeField] private int damageToEnemy = 1;
    [SerializeField] private int damageToBase = 10;
    [SerializeField] private int enemyScore;
    
    private Base _base;
    private GameState _gameState;

    private void Start()
    {
        _base = FindObjectOfType<Base>();
        _gameState = FindObjectOfType<GameState>();
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
        _gameState.AddToScore(enemyScore);
        Destroy(gameObject);
    }

    public void ExplodeEnemy()
    {
        Instantiate(enemyExplosionFx, transform.position, Quaternion.identity);
        _base.DamageBase(damageToBase);
        Destroy(gameObject);
    }
}
