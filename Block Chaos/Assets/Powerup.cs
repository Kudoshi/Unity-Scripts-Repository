using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public float speed;
    public float slowDown;
    public float lifeTime;
    private Transform target = null;
    private Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        Invoke("DestroySelf", lifeTime);
    }
    private void DestroySelf()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            target = other.transform;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && other.transform == null)
        {
            target = other.transform;
        }
    }
    private void FixedUpdate()
    {
        if (target != null)
        {

            Vector3 direction = target.position - transform.position;
            //rb.velocity = direction.normalized * speed;
            rb.AddForce(direction.normalized * speed);
        }
        else if (rb.velocity != Vector3.zero)
        {
            rb.AddForce(rb.velocity.normalized * (-slowDown));
            //rb.velocity = rb.velocity.normalized * (-slowDown);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            target = null;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.collider.GetComponent<PlayerPowerups>().onPickupPowerup();
            DestroySelf();
        }
    }
}
