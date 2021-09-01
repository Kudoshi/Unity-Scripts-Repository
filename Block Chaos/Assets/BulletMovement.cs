using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    private Bullet bullet;
    private float speed;
    private float bulletRange;
    private Vector3 spawnLocation;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        bullet = GetComponent<Bullet>();
        speed = bullet.speed;
        bulletRange = bullet.range;
        spawnLocation = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.forward * speed;
        //print("Init pos: " + transform.position + " | Curr pos: " + transform.position.x + bulletRange);
        if (spawnLocation.x >= transform.position.x + bulletRange || spawnLocation.x <= transform.position.x - bulletRange)
        {

            Destroy(gameObject);
        }
        if (spawnLocation.z >= transform.position.z + bulletRange || spawnLocation.z <= transform.position.z - bulletRange)
        {


            Destroy(gameObject);
        }
    }
}
