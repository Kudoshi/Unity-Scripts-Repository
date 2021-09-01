using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    /// <summary>
    /// Movement: Gets input in update. Moves charac in fixed update
    /// </summary>
    /// 
    [HideInInspector]
    public float speed;

    private Rigidbody rb;
    private Vector2 movementInput = new Vector2(0,0);

    private Vector3 refVelocity = Vector3.zero;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        speed = GetComponent<Player>().speed;   
    }
    private void FixedUpdate()
    {

        //Camera movement
        Plane plane = new Plane(Vector3.up, 0);

        float distance;
        Vector3 worldPosition = Vector3.zero; 
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out distance))
        {
            worldPosition = ray.GetPoint(distance);
        }

        var localTarget = transform.InverseTransformPoint(worldPosition);
        //Get angle
        var angle = Mathf.Atan2(localTarget.x   , localTarget.z) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + angle, transform.eulerAngles.z);


        //Character movement
        Vector3 targetVelocity = new Vector3(movementInput.x * speed * Time.deltaTime, 0, movementInput.y * speed * Time.deltaTime);
        rb.MovePosition(transform.position + targetVelocity);

        


    }
    private void Update()
    {
        //Get Movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        movementInput = new Vector2(horizontalInput, verticalInput);
    }
}
