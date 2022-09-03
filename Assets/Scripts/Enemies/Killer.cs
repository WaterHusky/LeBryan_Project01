using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killer : Enemy
{
    TankController _tankController;
    protected override void PlayerImpact(Player player)
    {
            player.Kill();
    }
}
