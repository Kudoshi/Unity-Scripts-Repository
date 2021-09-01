using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerPowerups : MonoBehaviour
{
    [Header("Attachment")]
    public GameObject powerTextPf;
    [Header("Powerup Text Info")]
    public Transform parentedTo;
    public float msgDissapearDur;
    public float msgMoveSpeed;
    public float msgFadeStartTime;
    [Header("Powerups Info")]
    public int block;
    public float maxHealth;
    public float maxHealthPerc;
    public float damage;
    public float damagePerc;
    public float shootRate;
    public float shootRatePerc;
    public float gunRange;
    public float gunRangePerc;
    public float gunSpeed;
    public float gunSpeedPerc;
    public float speed;
    public float speedPerc;
    public float life;
    public float lifePerc;

    private Player player;

    private void Awake()
    {
        player = GetComponent<Player>();
    }
    private void Start()
    {
        //Assign proper percentage
        damagePerc += maxHealthPerc;
        shootRatePerc += damagePerc;
        gunRangePerc += shootRatePerc;
        gunSpeedPerc += gunRangePerc;
        speedPerc += gunSpeedPerc;
        lifePerc += speedPerc;

        if (lifePerc > 100)
        {
            if (DebugMode.debugMode)
            {
                Debug.LogWarning("Player powerup percentage exceeded 100! Check again the value.");

            }
        }

    }

    public void onPickupPowerup()
    {
        GameObject textPf = Instantiate(powerTextPf, parentedTo);
        textPf.GetComponent<PickupTextDissapearAnimation>().msgDissapearDur = this.msgDissapearDur;
        textPf.GetComponent<PickupTextDissapearAnimation>().moveSpeed = this.msgMoveSpeed;
        textPf.GetComponent<PickupTextDissapearAnimation>().fadeDuration = this.msgFadeStartTime;

        player.UpdateStats("block", block);

        //Select powerup based on chance
        float randomChance = Random.Range(0f, 100.0f);
        if (randomChance <= maxHealthPerc)
        {
            textPf.GetComponent<TextMeshProUGUI>().text = "+ " + maxHealth + " max HP";
            player.UpdateStats("maxHealth", maxHealth);
        }
        else if (randomChance <= damagePerc)
        {
            textPf.GetComponent<TextMeshProUGUI>().text = "+ " + damage + " damage";
            player.UpdateStats("damage", damage);
        }
        else if (randomChance <= shootRatePerc)
        {
            textPf.GetComponent<TextMeshProUGUI>().text =  shootRate + " shoot cooldown";
            player.UpdateStats("shootcdTime", shootRate);
        }
        else if (randomChance <= gunRangePerc)
        {
            textPf.GetComponent<TextMeshProUGUI>().text = "+ " + gunRange + " shoot range";
            player.UpdateStats("gunRange", gunRange);
        }
        else if (randomChance <= gunSpeedPerc)
        {
            textPf.GetComponent<TextMeshProUGUI>().text = "+ " + gunSpeed + " bullet speed";
            player.UpdateStats("gunSpeed", gunSpeed);
        }
        else if (randomChance <= speedPerc)
        {
            textPf.GetComponent<TextMeshProUGUI>().text = "+ " + speed + " movement speed";
            player.UpdateStats("speed", speed);
        }
        else if (randomChance <= lifePerc)
        {
            textPf.GetComponent<TextMeshProUGUI>().text = "+ " + life + " hp";
            player.UpdateStats("currentHealth", life);
        }
        textPf.SetActive(true);
    }
}
