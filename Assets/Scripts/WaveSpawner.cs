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

    //      Trying to access the SpawnPointOne object and set a transform equal 
    //  to it so I can use it as the spawn point for enemies. If I do it this
    //  way then later if there needs to be 2 spawn points I can just add a 
    //  second variable and game object for it. Also the code at the bottom 
    //  won't work until I figure this part out so it's been commented out 
    //  for now. In the tutorial for this script he makes a random spawn 
    //  point generator, so since I want it to spawn in one spot I'm on 
    //  my own for this part. Maybe I have to make a new script, attach it to
    //  the spawn point object, and do the get component there. After that I
    //  can make the variable holding it static and pass it to the WaveSpawner class.
    // I already did a lot of this stuff also.

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
            Debug.Log("Potato 1");
            if (state != SpawnState.SPAWNING)
            {
                Debug.Log("Potato 2");
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

        if (nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
            Debug.Log("Waves Looping...");
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
        Debug.Log("Potato 3");
        Debug.Log("Spawning Wave: " + _wave.name);
            state = SpawnState.SPAWNING;
            for (int i = 0; i < _wave.count; i++)
            {
                SpawnEnemy(_wave.enemy);
                yield return new WaitForSeconds(1f / _wave.rate);
            Debug.Log("Potato 5");
        }
            state = SpawnState.FIGHTING;
            yield break;
        }

        void SpawnEnemy(Transform _enemy)
        {
        Debug.Log("Potato 4");
        Debug.Log("Spawning Enemy: " + _enemy.name);
        //Transform _sp = spawnPointOne; //Transform cannot turn into vector like this   
            //Instantiate(_enemy, _sp.position, _sp.rotation);
        }

}
