using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invincibility : PowerUpBase
{
    protected override void PowerUp(Player player)
    {
        player.InvincibleOn();
    }

    protected override void PowerDown(Player player)
    {
        player.InvincibleOff();
    }
}
