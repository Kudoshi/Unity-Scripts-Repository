using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private GameObject playerObj;
    private Rigidbody rb;
    public int speed = 15;
    void Start()
    {
        playerObj = GameObject.Find("Player");
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveToPlayer();
        if (transform.position.y < -10)
        {
            Destroy(this);
            GameObject.Find("SpawnManager").GetComponent<SpawnManager>().EnemyDies();
        }
    }
    private void MoveToPlayer()
    {
        Vector3 targetLocation = playerObj.transform.position;
        Vector3 Location = targetLocation - gameObject.transform.position;
        rb.AddForce(Location.normalized * speed);
    }
}
