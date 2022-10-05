using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Boss : MonoBehaviour
{

    float lastShotTime;

    public Transform player;
    public Transform bulletSpawn;
    public GameObject bulletPrefab;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private GameObject bossturretPivot;

    [SerializeField] private float _accuracy = 3;
    [SerializeField] float bulletForce = 25.0f;
    [SerializeField] float fireRate = 1.5f;

    private void Update()
    {
        if (Time.time - lastShotTime >= fireRate)
        {
            AimingPlayer(0);
            FireBullets();
        }

    }

    public void FireBullets()
    {

        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(bulletSpawn.forward * bulletForce, ForceMode.Impulse);

        lastShotTime = Time.time;


    }


    public void AimingPlayer(float Smoothing)
    {
        Vector3 playerPos = _playerTransform.position;

        // The target rotation "Fully Looking at player"
        Quaternion TargetRot = Quaternion.LookRotation((playerPos - bossturretPivot.transform.position).normalized);

        // If no smoothing, then snap
        if (Smoothing <= 0)
        {
            bossturretPivot.transform.rotation = TargetRot;
            return;
        }

        // Smooth rotation instead of snap
        bossturretPivot.transform.rotation = Quaternion.Lerp(bossturretPivot.transform.rotation, TargetRot, Smoothing * Time.fixedDeltaTime);

    }


}
