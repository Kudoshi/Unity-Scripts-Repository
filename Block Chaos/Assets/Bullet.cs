using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [HideInInspector]
    public float damage;
    [HideInInspector]
    public float speed;
    [HideInInspector] 
    public float range;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Enemy>())
        {
            other.GetComponent<Enemy>().OnDamaged(damage);
        }
        if (!other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
