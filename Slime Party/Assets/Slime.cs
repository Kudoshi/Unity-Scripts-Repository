using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Slime : MonoBehaviour
{
    
    public float speed;
    public float health;
    public float dmg;

    private behaviourMode _behaviourMode = behaviourMode.Idle;
    private Transform enemy = null;
    private Rigidbody rb;
    public List<Transform> enemyList = new List<Transform>();
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _behaviourMode = behaviourMode.Moving;
        StartCoroutine(Behaviour());
        Invoke("test", 5.0f);
    }

    private void test()
    {
        _behaviourMode = behaviourMode.Idle;
    }

    private void Update()
    {
        setAction();
    }

    private void setAction()
    {
       ////SET mode
        if (enemyList.Any())
        {
            _behaviourMode = behaviourMode.Attack;
        }

        if (_behaviourMode == behaviourMode.Attack & !enemyList.Any())
        {
            _behaviourMode = behaviourMode.Moving;
        }


        // //Move in one direction
        // //Get target position and my own position
        // Vector3 direction = enemy.position - transform.position;
        // Vector3 directionSpeed = direction * speed;
        // rb.velocity = directionSpeed;


    }

    IEnumerator Behaviour()
    {
        while (true)
        {
            //Move in direction
            if (_behaviourMode == behaviourMode.Moving)
            {
                rb.velocity = transform.forward * speed;
            }
            else if (_behaviourMode == behaviourMode.Attack)
            {
                attack();
                Debug.Log("ATTACK ENEMY: " + enemyList);
            }
            yield return new WaitForFixedUpdate();

        }
    }
    private void attack()
    {
        enemy = GetClosestEnemy();

    }


    private Transform GetClosestEnemy()
    {
        Transform target = null;
        float closestDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        
        foreach(Transform potentialtarget in enemyList)
        {
            Vector3 directionToTarget = potentialtarget.position - currentPosition;
            float distanceToTarget = directionToTarget.sqrMagnitude;
            if (distanceToTarget < closestDistance)
            {
                closestDistance = distanceToTarget;
                target = potentialtarget;
            }
        }

        return target;
    }
    public void UpdateCreep(Transform enemy, string operation)
    {
        if (operation == "add")
        {
            enemyList.Add(enemy);
        }
        else if (operation == "remove")
        {
            if (enemyList.Contains(enemy))
            {
                enemyList.Remove(enemy);
            }
            else
            {
                if (DebugMode.debugMode == true)
                {
                    Debug.Log("[CREEP ERROR] Enemy to be removed not found. ".Color("red" + "Enemy: " + enemy.name));
                }

            }

        }
    }

    private enum behaviourMode
    {
        Moving, Attack, Idle
    }

}
