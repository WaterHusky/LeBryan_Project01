using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inheritance
{
    public class AcceleratorMissile : Projectile
    {
        [Header("Accelerator Missile Settings")]
        [SerializeField] float _accelerationAmount = .01f;
        [SerializeField] int _damageAmount = 10;

        private void Start()
        {
            TravelSpeed = 0;
        }

        protected override void Move()
        {
            // increase speed
            TravelSpeed += _accelerationAmount;

            base.Move();
            // or optionally we could not call base and just
            // completely change the movement
        }

        protected override void Impact(Collision otherCollision)
        {
            Debug.Log("Missile deals " + _damageAmount + " damage to " +
                otherCollision.gameObject.name + "!");
            gameObject.SetActive(false);
            // optionally search collision for component with GetComponent
            // other.gameObject.GetComponent<ComponentToSearch>()
        }
    }
}

