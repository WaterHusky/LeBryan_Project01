using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Minion : MonoBehaviour
{
    TankController player;
    [SerializeField] private int physicalMinionDamage;
    int MoveSpeed = 4;


    private void Awake()
    {
        SetTarget();
    }
    void Update()
    {
        Vector3 playerPos = player.transform.position;
        transform.LookAt(player.transform);

        transform.position = Vector3.MoveTowards(transform.position, playerPos, MoveSpeed * Time.deltaTime);

    }

    private void OnCollisionEnter(Collision collision)
    {
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        damageable?.TakeDamage(physicalMinionDamage);
    }

    public void SetTarget()
    {
        player = FindObjectOfType<TankController>();
    }
}
