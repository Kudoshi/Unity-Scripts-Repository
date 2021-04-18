using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f;
    public bool hasPowerup = false;
    private Rigidbody rb;
    private GameObject focalPoint;
    public float blastSpeed = 2.0f;
    public float powerupForce = 5.0f;
    private bool hasBoost = true;

    public GameObject boostFX;
    public GameObject powerupFX;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("FocalPoint");
        powerupFX.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 movementDir = new Vector3(horizontalInput, 0, verticalInput);
        rb.AddForce((movementDir * speed));

        boostFX.transform.position = new Vector3(transform.position.x, boostFX.transform.position.y, transform.position.z);
        powerupFX.transform.position = new Vector3(transform.position.x, powerupFX.transform.position.y, transform.position.z);

        if (Input.GetKeyDown(KeyCode.Space) && hasBoost)
        {
            rb.AddForce(movementDir * blastSpeed, ForceMode.Impulse);
            StartCoroutine(BoostCooldown());
        }
    }
    IEnumerator BoostCooldown()
    {
        hasBoost = false;
        boostFX.SetActive(false);
        yield return new WaitForSeconds(2.5f);
        hasBoost = true;
        boostFX.SetActive(true);

    }
    IEnumerator SkillCooldown()
    {
        hasPowerup = true;
        powerupFX.SetActive(true);
        yield return new WaitForSeconds(6.0f);
        hasPowerup = false;
        powerupFX.SetActive(false);

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
            StartCoroutine(SkillCooldown());
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (hasPowerup == true)
            {
                Debug.Log("Collided with enemy with powerup");
                Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
                Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;
                enemyRb.AddForce(awayFromPlayer * powerupForce, ForceMode.Impulse);
            }
        }
    }
}
