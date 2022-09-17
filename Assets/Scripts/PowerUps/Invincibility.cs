using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invincibility : PowerUpBase
{
    protected override void PowerUp(TankController player)
    {
        player.InvincibleOn();
    }

    protected override void PowerDown(TankController player)
    {
        player.InvincibleOff();
    }
}
