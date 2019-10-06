using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private int enemyHitPoints = 15;
    [SerializeField] private GameObject enemyExplosionFx;
    [SerializeField] private GameObject enemyDamageFx;
    private int _damage = 1;
    
    private void OnParticleCollision(GameObject otherCollider)
    {
        DamageEnemy(_damage, otherCollider.transform);  //todo: get damage from colliding object
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
        Instantiate(enemyExplosionFx, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
