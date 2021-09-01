using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletPf;
    public Transform bulletSpawnLocation;
    private Player player;
    [HideInInspector]
    public float cdTime;
    [HideInInspector]
    public float bulletDmg;
    [HideInInspector]
    public float bulletSpeed;
    [HideInInspector]
    public float bulletRange;

    private float nextFireTime = 0.0f;
    // Start is called before the first frame update
    void Awake()
    {
        player = GetComponent<Player>();
        cdTime = player.shootcdTime;
        bulletDmg = player.damage;
        bulletSpeed = player.gunSpeed;
        bulletRange = player.gunRange;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Shoot") && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + cdTime;
        }
    }

    private void Shoot()
    {

        GameObject bullet = Instantiate(bulletPf, bulletSpawnLocation.position,transform.rotation);
        Bullet bulletInfo = bullet.GetComponent<Bullet>();
        bulletInfo.damage = bulletDmg;
        bulletInfo.speed = bulletSpeed;
        bulletInfo.range = bulletRange;

        AudioManager.onPlayOneShotSound(gameObject, "Player_Shoot");
    }
}
