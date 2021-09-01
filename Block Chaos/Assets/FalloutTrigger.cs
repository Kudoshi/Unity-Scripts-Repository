using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FalloutTrigger : MonoBehaviour
{
    public GameObject playerCam;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            player.GetComponent<PlayerSpawnBlock>().enabled = false;
            player.GetComponent<PlayerMovement>().enabled = false;
            playerCam.SetActive(false);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.collider.GetComponent<Player>().onHit(99999999999999);
        }
        else
        {
            Destroy(collision.collider.gameObject);
        }

    }
}
