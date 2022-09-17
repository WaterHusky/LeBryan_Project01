using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthIncrease : CollectibleBase
{
    [SerializeField] int _healthAdded = 1;

    protected override void Collect(TankController player)
    {
        Health health = player.GetComponent<Health>();
        health.Heal(_healthAdded);
    }
}
