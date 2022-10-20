using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{

    private enum SpawnState { SPAWNING, FIGHTING, COUNTING };

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;
    }

    public Wave[] waves;
    private int nextWave = 0;

    public Transform spawnPointOne = GetSpawnPoint.spawnPoint;

    public float timeBetweenWaves = 5f;
    public float waveCountdown;

    private float searchCountdown = 1f;

    private SpawnState state = SpawnState.COUNTING;

    void Start()
    {
        waveCountdown = timeBetweenWaves;
    }

    void Update()
    {
        if (state == SpawnState.FIGHTING)
        {
            if (!EnemyIsAlive())
            {
                WaveCompleted();
            }
            else
            {
                return;
            }
        }
        if (waveCountdown <= 0)
        {
  
            if (state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            if (waveCountdown - Time.deltaTime > 0)
            {
                waveCountdown -= Time.deltaTime;
            }
            else {
                waveCountdown = 0;
            }
        }
    }

    void WaveCompleted() 
    {
        Debug.Log("Wave Completed");
        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if (nextWave + 1 > waves.Length - 1) //This is where the looping happens
        {
            return; //If I put return here then it loops wave 3 forever
           // nextWave = 0;
           // Debug.Log("Waves Looping...");
        }
        else { 
            nextWave++;
        }
    }

        bool EnemyIsAlive()
        {
            searchCountdown -= Time.deltaTime;
            if (searchCountdown <= 0f)
            {
                searchCountdown = 1f;
                if (GameObject.FindGameObjectWithTag("MinorEnemy") == null)
                { //You might have to make an if statement for each enemy tag
                    return false;
                }
            }

            return true;
        }

        IEnumerator SpawnWave(Wave _wave)
        {
        Debug.Log("Spawning Wave: " + _wave.name);
            state = SpawnState.SPAWNING;
            for (int i = 0; i < _wave.count; i++)
            {
                SpawnEnemy(_wave.enemy);
                yield return new WaitForSeconds( 1f/_wave.rate );
            }
        state = SpawnState.FIGHTING;
        }

        void SpawnEnemy(Transform _enemy)
        {
        Debug.Log("Spawning Enemy: " + _enemy.name);
        Transform _sp = spawnPointOne;   
        Instantiate(_enemy, _sp.position, _sp.rotation);
        }

}
