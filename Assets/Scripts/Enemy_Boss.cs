using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Boss : MonoBehaviour
{
    int fireCount = 0;
    float lastShotTime;

    public Transform player;
    public Transform bulletSpawn;
    public GameObject bulletPrefab;

    [SerializeField] float bulletForce = 25.0f;
    [SerializeField] float fireRate = 1.5f;

    private void Start()
    {

    }
    private void Update()
    {
        if (Time.time - lastShotTime >= fireRate)
        {
            FireBullets();
        }
    }

    public void FireBullets()
    {
        // shoot 2 consecutive bullets

        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(bulletSpawn.forward * bulletForce, ForceMode.Impulse);

        lastShotTime = Time.time;
        fireCount++;

        if (fireCount == 2)
        {
            FireBullet();
            fireCount = 0;
        }
    }

    public void FireBullet()
    {
        // shoot bullet projectile at player


    }
}
