using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slower : Enemy
{
    
    [SerializeField] private float _SlowingSpeed = 0.5f;

    protected override void PlayerImpact(Player player)
    {
        //base.PlayerImpact(player);
        TankController controller = player.GetComponent<TankController>();
        if (controller != null)
        {
            controller.MaxSpeed -= _SlowingSpeed;
        }

    }
}
