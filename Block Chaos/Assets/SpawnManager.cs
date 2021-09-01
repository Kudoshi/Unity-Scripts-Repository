using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;
using TMPro;
public class SpawnManager : MonoBehaviour
{
    
    public float gracePeriod;
    public List<UnitSpawn> unitList;
    public float repeatSpawningDur;
    public int repeatSpawningSpawnCount;

    [Header("Attachment")]
    public TextMeshProUGUI topMsg;
    public GameTimer gameTimer;
    [Header("View Only")]
    public float startTime;



    
    // Start is called before the first frame update
    void Start()
    {
        
        
        StartCoroutine(StartEnemySpawnTimer());
        Events.invokeOnInfoTopMsg(("Game starts in " + gracePeriod + " seconds"), 5);
        gameTimer.StartTimer(gracePeriod, "graceCountDown");
    }
    
    private IEnumerator StartEnemySpawnTimer()
    {
        yield return new WaitForSeconds(gracePeriod);
        gameTimer.StartTimer(0, "timer");
        int currentSpawnCount = 0;
        while (GameManager.gameOn)
        {
            foreach (UnitSpawn unit in unitList)
            {
                yield return new WaitForSeconds(unit.spawnGap);
                StartCoroutine(SpawnEnemy(unit, currentSpawnCount));
            }
            yield return new WaitForSeconds(repeatSpawningDur);
            repeatSpawningDur /= 2;
            currentSpawnCount *= 2;
        }
    }
    private IEnumerator SpawnEnemy(UnitSpawn unit, int currentSpawning = 0)
    {
        while (GameManager.gameOn)
        {
            currentSpawning += 1;
            GameObject enemy = Instantiate(unit.unitPf, transform.position, transform.rotation);
            //Set atribute
            Enemy enemyScript = enemy.GetComponent<Enemy>();
            enemyScript.maxHealth = unit.maxHealth + (unit.maxHealthInc * currentSpawning-1);
            enemyScript.damage = unit.damage + (unit.damageInc * currentSpawning - 1);
            enemyScript.scorePoint = unit.score + (unit.scoreInc * currentSpawning - 1);

            //Set move speed
            enemy.GetComponent<NavMeshAgent>().speed = unit.speed + (unit.speedInc * currentSpawning - 1);

            yield return new WaitForSeconds(unit.spawnRate);
        }
        yield return null;
    }
    // Update is called once per frame
    void Update()
    {
        if (DebugMode.debugMode)
        {
            //if (Input.GetKeyDown("v"))
            //{
            //    UnitSpawn unit = unitList.Find(element => element.name == "Zombie");
            //    StartCoroutine(SpawnEnemy(unit, 0));

            //}
            //if (Input.GetKeyDown("b"))
            //{
            //    UnitSpawn unit = unitList.Find(element => element.name == "Zerg");
            //    StartCoroutine(SpawnEnemy(unit, 0));

            //}
            //if (Input.GetKeyDown("n"))
            //{
            //    UnitSpawn unit = unitList.Find(element => element.name == "Tank");
            //    StartCoroutine(SpawnEnemy(unit, 0));

            //}
            //if (Input.GetKeyDown("m"))
            //{
            //    UnitSpawn unit = unitList.Find(element => element.name == "Demon");
            //    StartCoroutine(SpawnEnemy(unit, 0));

            //}

        }
    }
}

[System.Serializable]
public class UnitSpawn
{
    public string name;
    public GameObject unitPf;
    [Header("SpawnInfo")]
    public float spawnGap;
    public float spawnRate;
    [Header("Enemy Attributes")]
    public float speed;
    public float speedInc;
    public float maxHealth;
    public float maxHealthInc;
    public float damage;
    public float damageInc;
    public int score;
    public int scoreInc;

}
