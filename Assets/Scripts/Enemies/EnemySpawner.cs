using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] EnemyPrefabs;
    float spawns;
    bool canSpawn;

    public void SpawnEnemy()
    {
        canSpawn = !canSpawn;

        if (canSpawn)
        {
            for (int i = 0; i < spawns; i++)
            {
                Instantiate(EnemyPrefabs[Random.Range(0, EnemyPrefabs.Length)], transform.position, Quaternion.identity);
            }
            spawns += 0.2f;
        }
    }
}
