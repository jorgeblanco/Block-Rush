using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyToSpawn;
    [SerializeField] private float spawnTime = 3f;
    [SerializeField] private int spawnCount = 10;

    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        while (spawnCount > 0)
        {
            var enemy = Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
            enemy.transform.parent = transform.parent;
            spawnCount--;
            yield return new WaitForSeconds(spawnTime);
        }
    }
}
