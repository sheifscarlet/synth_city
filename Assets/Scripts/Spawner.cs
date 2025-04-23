using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    
    [SerializeField] Transform player;
    [SerializeField] float minX;
    [SerializeField] float maxX;
    [SerializeField] float minY;
    [SerializeField] float maxY;
    private float timeBetweenSpawn;
    [SerializeField] private float minTime;
    [SerializeField] private float maxTime;
    private float spawnTime;

    private void Start()
    {
        RndSpawnTime();
    }

    // Update is called once per frame
    void Update()
    {
        // timeBetweenSpawn = Random.Range(minTime, maxTime);
        // Debug.Log(timeBetweenSpawn);
        if (Time.time > spawnTime)
        {
            Spawn();
            spawnTime = Time.time + timeBetweenSpawn;
        }
    }

    private void Spawn()
    {
        float randX = Random.Range(minX, maxX);
        float randY = Random.Range(minY, maxY);
        GameObject newEnemy = Instantiate(enemy, new Vector3(randX, randY, 0), transform.rotation);
        
        MeeleEnemyController enemyController = newEnemy.GetComponent<MeeleEnemyController>();
        enemyController.Setup(player);
        RndSpawnTime();
    }

    void RndSpawnTime()
    {
        timeBetweenSpawn = Random.Range(minTime, maxTime);
        Debug.Log(timeBetweenSpawn);
    }
}
