using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inheritance
{
   // [RequireComponent(typeof(Rigidbody))]
    public abstract class Projectile : MonoBehaviour
    {
        protected abstract void Impact(Collision otherCollision);

        [Header("Base Settings")]
        [SerializeField] protected float TravelSpeed = .25f;
        [SerializeField] protected Rigidbody RB;

        [Header("FX")]
        [SerializeField] protected AudioClip _impactSound;
        [SerializeField] protected ParticleSystem _impactParticle;


        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("Projectile collision!");
            //possible object filtering
            Impact(collision);
            if(_impactParticle != null)
            {
                //play particle
            }
            if (_impactSound != null)
            {
                //play sound effect
            }

        }

     //   private void Awake()
       // {
       //     if (RB == null)
       //     {
       //         RB = GetComponent<Rigidbody>();
       //     }
       // }

        private void FixedUpdate()
        {
            Move();
        }

        protected virtual void Move()
        {
            Vector3 moveOffset = transform.forward * TravelSpeed;
            RB.MovePosition(RB.position + moveOffset);
        }
    }
}

