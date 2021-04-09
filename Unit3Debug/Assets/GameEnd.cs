using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnd : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            PlayerControllerX playerScript = collision.collider.GetComponent<PlayerControllerX>();
            playerScript.gameOver = true;
            playerScript.playerAudio.PlayOneShot(playerScript.explodeSound, 1.0f);
            playerScript.explosionParticle.Play();
        }
    }
}
