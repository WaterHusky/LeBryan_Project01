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
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private GameObject bossturretPivot;

    [SerializeField] private float _accuracy = 3;
    [SerializeField] float bulletForce = 25.0f;
    [SerializeField] float fireRate = 1.5f;

    private void Start()
    {

    }
    private void Update()
    {
        if (Time.time - lastShotTime >= fireRate)
        {
            AimingPlayer();
            FireBullets();
        }

    }

    public void FireBullets()
    {

        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(bulletSpawn.forward * bulletForce, ForceMode.Impulse);

        lastShotTime = Time.time;
        fireCount++;

    }

    public void AimingPlayer()
    {
        Vector3 playerPos = _playerTransform.position;

        bossturretPivot.transform.LookAt(new Vector3(0, playerPos.y, 0));
    }

   
}
