using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{
    public List<Waves> waves;
    public int startingWave = 0;
    public int timeBWwaves = 3;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnAllWaves());
    }

    private IEnumerator SpawnAllWaves()
    {
        for(int waveIndex=startingWave; waveIndex< waves.Count; waveIndex++)
        {
            var currentWave = waves[waveIndex];
            yield return StartCoroutine(SpawnWave(currentWave));
        }
        yield return new WaitForSeconds(8);
        SceneManager.LoadScene("Game Over");
    }

    private IEnumerator SpawnWave(Waves whichWave)
    {
        for(int enemyCount=0; enemyCount<whichWave.NumOfEnemies(); enemyCount++)
        {
            var newEnemy =  Instantiate(whichWave.GetEnemyPrefab(),
            whichWave.GetWaypoints()[0].transform.position,
            Quaternion.identity);
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(whichWave);
            yield return new WaitForSeconds(whichWave.GetTimeBWSpawn());
        }
        yield return new WaitForSeconds(timeBWwaves);
    }
}
