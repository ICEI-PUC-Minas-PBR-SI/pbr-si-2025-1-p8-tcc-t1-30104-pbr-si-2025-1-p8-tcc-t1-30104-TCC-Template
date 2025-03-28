using UnityEngine;
using System.Collections;

public class SpawnController : MonoBehaviour
{
    public GameObject[] enemies;
    public Transform[] spawnPoints;
    public float spawnInterval = 10f; 

    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        if (spawnPoints.Length == 0 || enemies.Length == 0) return;
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject enemyPrefab = enemies[Random.Range(0, enemies.Length)];

        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
    }
}
