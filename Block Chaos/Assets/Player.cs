using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Player : MonoBehaviour
{
    public float maxHealth;
    public int startingBlock;
    [HideInInspector]
    public int currentBlock;
    private float currentHealth;
    public float speed;
    public float injuredCdRate;
    public float injuredKnockback;
    public float injuredKnockbackDuration;
    public float gravity;
    [Header("Gun Info")]
    public float damage;
    public float shootcdTime;
    public float gunRange;
    public float gunSpeed;
    
    [Header("Attachment")]
    public HPBarSlider hpBar;
    public TextMeshProUGUI blockText;
    public GameManager gameManager;

    private PlayerShoot playerShoot;
    private PlayerMovement playerMovement;

    private float nextInjuredTime;
    private Rigidbody rb;
    private Vector3 knockbacked = Vector3.zero;
    private float stopKnockbackTime = 0;
    private void Awake()
    {
        currentHealth = maxHealth;
        currentBlock = startingBlock;
        playerShoot = GetComponent<PlayerShoot>();
        playerMovement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update

    private void Start()
    {
        //Set blockText
        blockText.text = "x " + currentBlock;
    }
    public void onHit(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            AudioManager.PlaySound(gameObject, "Player_Die");
            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<PlayerShoot>().enabled = false;
            GetComponent<PlayerSpawnBlock>().enabled = false;
            GetComponent<Player>().enabled = false;
            GetComponent<PlayerPowerups>().enabled = false;
            transform.Find("Model").gameObject.SetActive(false);
            gameManager.triggerEndGame();
        }
        else
        {
            AudioManager.PlayOneShotSound(gameObject, "Player_Hit");
        }
        hpBar.UpdateValue(currentHealth, maxHealth);
    }
    private void FixedUpdate()
    {
        if (knockbacked != Vector3.zero && Time.time <= stopKnockbackTime)
        {
            rb.AddForce(knockbacked*injuredKnockback, ForceMode.Impulse);
        }
        else if(Time.time >= stopKnockbackTime)
        {
            knockbacked = Vector3.zero;
        }
        if (transform.position.y >=0 )
        {
            rb.velocity = new Vector3(rb.velocity.x, -gravity, rb.velocity.z);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy") && Time.time >= nextInjuredTime)
        {
            onHit(collision.collider.GetComponent<Enemy>().damage);
            nextInjuredTime = Time.time + injuredCdRate;
            Vector3 direction = transform.position - collision.transform.position;
            knockbacked = new Vector3(direction.x, 10, direction.z);
            stopKnockbackTime = Time.time + injuredKnockbackDuration;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy") && Time.time >= nextInjuredTime)
        {
            onHit(collision.collider.GetComponent<Enemy>().damage);
            nextInjuredTime = Time.time + injuredCdRate;

            Vector3 direction = transform.position - collision.transform.position;
            knockbacked = new Vector3(direction.x, 10, direction.z);
            stopKnockbackTime = Time.time + injuredKnockbackDuration;
        }
    }

    public void UpdateStats(string attribute, float amt)
    {
        if (attribute == "block")
        {
            currentBlock += (int)amt;
            blockText.text = "x " + currentBlock;
        }
        else if (attribute == "maxHealth")
        {
            maxHealth += amt;
            hpBar.UpdateValue(currentHealth, maxHealth);
            AudioManager.PlayOneShotSound(gameObject, "Player_Powerup");
        }
        else if (attribute == "speed")
        {
            speed += amt;
            playerMovement.speed = speed;
            AudioManager.PlayOneShotSound(gameObject, "Player_Powerup");
        }
        else if (attribute == "damage")
        {
            damage += amt;
            playerShoot.bulletDmg = damage;
            AudioManager.PlayOneShotSound(gameObject, "Player_Powerup");
        }
        else if (attribute == "shootcdTime")
        {
            if (shootcdTime + amt <= 0.1)
            {
                //Ignore adding amt
            }
            else
            {
                shootcdTime += amt;
                playerShoot.cdTime = shootcdTime;
            }

            AudioManager.PlayOneShotSound(gameObject, "Player_Powerup");
        }
        else if (attribute == "gunRange")
        {
            gunRange += amt;
            playerShoot.bulletRange = gunRange;
            AudioManager.PlayOneShotSound(gameObject, "Player_Powerup");
        }
        else if (attribute == "gunSpeed")
        {
            gunSpeed += amt;
            playerShoot.bulletSpeed = gunSpeed;
            AudioManager.PlayOneShotSound(gameObject, "Player_Powerup");
        }
        else if (attribute == "currentHealth")
        {
            currentHealth += amt;
            hpBar.UpdateValue(currentHealth, maxHealth);
            AudioManager.PlayOneShotSound(gameObject, "Player_Powerup");
        }
        else
        {
            if (DebugMode.debugMode)
            {
                Debug.LogWarning("[PLAYER ERROR] Update attribute: "+ attribute+" - is not a valid attribute");
            }
        }
       
    }
}
