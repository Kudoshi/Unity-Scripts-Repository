using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindAndAttack : MonoBehaviour
{
    public Transform enemy;
    public float speed;
    private Rigidbody rb;
    public List<Transform> enemyList = new List<Transform>();
    //var dir : vector3 = target.position - transform.position;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //Get target position and my own position
        Vector3 direction = enemy.position - transform.position;
        Vector3 directionSpeed = direction * speed;
        rb.velocity = directionSpeed;

    }

    public void UpdateCreep(Transform enemy, string operation)
    {
        if (operation == "add")
        {
            enemyList.Add(enemy);
            Debug.Log("Enemy added: " + enemy.name);
        }
        else if (operation == "remove")
        {
            if (enemyList.Contains(enemy))
            {
                enemyList.Remove(enemy);
                Debug.Log("Removed!");
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
}
