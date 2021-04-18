using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerDemo : MonoBehaviour
{
    private Rigidbody playerRb;
    public GameObject focalPoint;
    private int speed = 35;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Getting vertical and horizontal input and storing them as Vector3
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 InputVal = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        Debug.Log("Input Val = " + InputVal);

        //Calculate angle between current angle to the target angle
        //float calcAngle = Mathf.Atan2(InputVal.x, InputVal.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

        //Debug.Log("Angle turned : " + calcAngle);
        //line below not needed as I want the ball to rotate freely based on physicsg
        //float angleTo = Mathf.SmoothDampAngle(transform.eulerAngles.y, calcAngle, ref turnSmoothVelocity, turnSmoothTime); //Smooth the turning to the angle
        //transform.rotation = Quaternion.Euler(transform.rotation.x, angleTo, transform.rotation.z); //Rotate to the target angle

        //Rotates the internal focal point instead of actual player
        // focalPoint.transform.rotation = Quaternion.Euler(0f, calcAngle, 0f);
        if (verticalInput != 0.0f || horizontalInput != 0.0f)
        {
            //Adding force (focal point's forward direction + Input Val(direction we want to go based on the focal point)) times by speed and delta time
            playerRb.AddForce(focalPoint.transform.forward * speed);
            Debug.Log("Forca added: " + (focalPoint.transform.forward + InputVal * 2));


            Debug.Log("Focal point forward blue axis: " + focalPoint.transform.forward);


        }
    }
}
