using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject obstaclePrefab;
    private Vector3 spawnPos = new Vector3(30, 0, 0);
    private SideScrollMovement sideScrollMovement;

    void Start()
    {
        sideScrollMovement = GameObject.Find("Player").GetComponent<SideScrollMovement>();
        InvokeRepeating("SpawnObstacle", 1.0f, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SpawnObstacle()
    {
        if (sideScrollMovement.gameOver == false)
        {
            Instantiate(obstaclePrefab, spawnPos, obstaclePrefab.transform.rotation);

        }

    }
}
