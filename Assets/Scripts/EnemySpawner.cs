using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    /*
    public Enemy2 Slime;
    public List<Transform> spawnPoints;
    public float spawnPerTime;
    public int enemiesToSpawn;

    private bool isSpawning = true; // 후에 게임매니저의 GameOver체크로 변경
    private float spawnTimer = 0f;
    private int enemiesRemainingAlive;
    private int enemiesRemainingToSpawn;
    private List<Enemy2> spawnedEnemyList = new List<Enemy2>();
    private List<SpawnPoints> spawnPointList;

    // Start is called before the first frame update
    void Start()
    {
        enemiesRemainingToSpawn = enemiesToSpawn;
        SpawnPoints[] spawns = FindObjectsOfType<SpawnPoints>();
        for(int i = 0; i < spawns.Length; i++)
        {
            spawnPointList.Add(spawns[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isSpawning)
        {
            if(spawnTimer >= spawnPerTime)
            {
                SpawnEnemy(Slime);
                spawnTimer = 0f;
            }
            else
            {
                spawnTimer += Time.deltaTime;
            }
        }
    }

    private void WaveOver()
    {
        
    }

    private void SpawnEnemy(Enemy2 enemy)
    {
        int r = Random.Range(0, spawnPoints.Count);
        Enemy2 _enemy = Instantiate(enemy, spawnPoints[r].position, Quaternion.identity) as Enemy2;
        _enemy.Init(new Vector3(spawnPoints[r].position.x, 0f, spawnPoints[r].position.z));
        _enemy.onDeath += onEnemyDeath;

        enemiesRemainingToSpawn--;
        if (enemiesRemainingToSpawn <= 0) isSpawning = false;

        spawnedEnemyList.Add(_enemy);
    }

    private void onEnemyDeath()
    {
        enemiesRemainingAlive--;
        if(enemiesRemainingAlive == 0)
        {
            WaveOver();
        }
    }
    */
}
