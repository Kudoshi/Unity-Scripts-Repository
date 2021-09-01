using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    public float maxHealth;
    public float damage;
    public int scorePoint;
    public float obstacleHitRate;

    private float currentHealth;
    [Header("Attachment")]
    public HPBarSlider hpBar;
    public GameObject powerupPf;
    public GameObject deadObj;

    private Listener_Audio audioListener;
    // FOR AUDIO GO ACCORDING TO THIS LIST
    // 0 - SPAWN 1 - HIT 2 - DIE 3++ Anything else?
    private void Awake()
    {
        audioListener = GetComponent<Listener_Audio>();
    }
    void Start()
    {
        //Update hp bar
        currentHealth = maxHealth;
        hpBar.UpdateValue(currentHealth, maxHealth);


    }

    public void OnDamaged(float damage)
    {
        //print("On damaged");
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            GetComponent<BoxCollider>().enabled = false;
            FindObjectOfType<ScoreSystem>().onUnitKilled(gameObject, scorePoint);
            Instantiate(powerupPf, new Vector3(transform.position.x, 2, transform.position.z), powerupPf.transform.rotation);

            GameObject unitDie = new GameObject("UnitDie");
            Listener_Audio listener =  unitDie.AddComponent<Listener_Audio>();
            if (listener == null)
            {
                Debug.Log("Listener not found");
            }

            listener.audioRepo = audioListener.audioRepo;
            listener.soundList.Add(audioListener.soundList[2]);

            listener.playOnStart.Add(audioListener.soundList[2]);
            listener.dieAfterPlaySound = audioListener.soundList[2];

            Destroy(gameObject);
            
        }
        else
        {
            AudioManager.PlaySound(gameObject, audioListener.soundList[1]);
        }
        hpBar.UpdateValue(currentHealth, maxHealth);
    }
}
