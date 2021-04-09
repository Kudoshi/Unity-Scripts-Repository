using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject prefab;
    public GameObject powerup;
    public float spawnRange = 9.0f;
    private int enemyWave = 1;
    public int enemyCount = 0;
    private bool gameStart = false;
    private int targetWavePowerupSpawn = 1;

    void Start()
    {
        StartCoroutine(StartWave());
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
    public void EnemyDies()
    {
        enemyCount--;
        if (enemyCount == 0)
        {
            EndWave();
        }
    }
    //Wave manager
    IEnumerator StartWave()
    {

        yield return new WaitForSeconds(1.5f);
        SpawnPowerup();

        SpawnEnemy(enemyWave);
        if (gameStart == false)
        {
            gameStart = true;
        }
        
    }
    private void SpawnPowerup()
    {
        if (enemyWave == targetWavePowerupSpawn)
        {
            Vector3 spawnLocation = ReturnRandomLocation();
            Instantiate(powerup, spawnLocation, powerup.transform.rotation);
        }
    }
    private void EndWave()
    {
        CheckPowerupSpawn();

        enemyWave++;
        StartCoroutine(StartWave());
    }
    private void CheckPowerupSpawn()
    {
        GameObject powerupObj = GameObject.Find("PowerupObj");
        if (powerupObj == null)
        {
            Debug.Log("Powerupobj null");
            targetWavePowerupSpawn = (enemyWave + 2);
        }
    }
    private void SpawnEnemy(int enemyNumber)
    {
        
        for (int i = 0; i<enemyNumber; i++)
        {
            Vector3 spawnLocation = ReturnRandomLocation();
            enemyCount++;
            Instantiate(prefab, spawnLocation, prefab.transform.rotation);
        }
    }

    private Vector3 ReturnRandomLocation()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosY = Random.Range(-spawnRange, spawnRange);

        Vector3 spawnLocation = new Vector3(spawnPosX, 0, spawnPosY);

        return spawnLocation;
    }
}
