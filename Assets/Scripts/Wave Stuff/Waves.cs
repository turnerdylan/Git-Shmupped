using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Waves")]
public class Waves : ScriptableObject
{
    public GameObject enemyPrefab;
    public GameObject pathPrefab;
    public float timeBetweenSpawns = 0.5f;
    public float spawnRandomFactor = 0.3f;
    public int numberOfEnemies = 5;
    public float moveSpeed = 5f;

    public GameObject GetEnemyPrefab()
    {
        return enemyPrefab;
    }

    public List<Transform> GetWaypoints()
    {
        var waveWaypoints = new List<Transform>();
        foreach(Transform child in pathPrefab.transform)
        {
            waveWaypoints.Add(child);
        }
        return waveWaypoints;
    }
     
    public float GetTimeBWSpawn()
    {
        return timeBetweenSpawns + Random.Range(-spawnRandomFactor, spawnRandomFactor);
    }

    public float GetSpawnRandom()
    {
        return spawnRandomFactor;
    }

    public int NumOfEnemies()
    {
        return numberOfEnemies;
    }

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }
}
