using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    private Rigidbody playerRb;
    private float speed = 10;
    private GameObject focalPoint;

    public bool hasPowerup;
    public GameObject powerupIndicator;
    public int powerUpDuration = 5;

    [Header("Attachment")]
    public Transform cam;

    private float normalStrength = 15; // how hard to hit enemy without powerup
    private float powerupStrength = 30; // how hard to hit enemy with powerup

    private float turnSmoothVelocity = 0.5f;
    private float turnSmoothTime = 0.1f;
    
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Movement();
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.6f, 0);

    }
    private void Movement()
    {
        // Getting vertical and horizontal input and storing them as Vector3
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 InputVal = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        Debug.Log("Input Val = " + InputVal);

        //Calculate angle between current angle to the target angle
        float calcAngle = Mathf.Atan2(InputVal.x, InputVal.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

        //Debug.Log("Angle turned : " + calcAngle);
        //line below not needed as I want the ball to rotate freely based on physicsg
        //float angleTo = Mathf.SmoothDampAngle(transform.eulerAngles.y, calcAngle, ref turnSmoothVelocity, turnSmoothTime); //Smooth the turning to the angle
        //transform.rotation = Quaternion.Euler(transform.rotation.x, angleTo, transform.rotation.z); //Rotate to the target angle

        //Rotates the internal focal point instead of actual player
        focalPoint.transform.rotation = Quaternion.Euler(0f, calcAngle, 0f);
        if (verticalInput != 0.0f || horizontalInput != 0.0f)
        {
            //Adding force (focal point's forward direction + Input Val(direction we want to go based on the focal point)) times by speed and delta time
            playerRb.AddForce(focalPoint.transform.forward * speed);



            // Set powerup indicator position to beneath player
        }

    }
    // If Player collides with powerup, activate powerup
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
            hasPowerup = true;
            powerupIndicator.SetActive(true);
            StartCoroutine(PowerupCooldown());
        }
    }

    // Coroutine to count down powerup duration
    IEnumerator PowerupCooldown()
    {
        yield return new WaitForSeconds(powerUpDuration);
        hasPowerup = false;
        powerupIndicator.SetActive(false);
    }

    // If Player collides with enemy
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Rigidbody enemyRigidbody = other.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = other.gameObject.transform.position - transform.position; 
           
            if (hasPowerup) // if have powerup hit enemy with powerup force
            {
                enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
            }
            else // if no powerup, hit enemy with normal strength 
            {
                enemyRigidbody.AddForce(awayFromPlayer * normalStrength, ForceMode.Impulse);
            }


        }
    }



}
