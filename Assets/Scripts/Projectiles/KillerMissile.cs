using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inheritance
{
    public class KillerMissile : Projectile
    {
        protected override void Impact(Collision otherCollision)
        {
            Debug.Log("Bullet hit "
                + otherCollision.gameObject.name + "!");
            otherCollision.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}

