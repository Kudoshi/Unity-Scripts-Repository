using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitbox : MonoBehaviour
{
    public float hitCdRate;
    public Player player;
    private float nextInjuredTime = 0.0f;

    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && Time.time >= nextInjuredTime)
        {
            player.onHit(other.GetComponent<Enemy>().damage);
            nextInjuredTime = Time.time + hitCdRate;
        }
    }
}
