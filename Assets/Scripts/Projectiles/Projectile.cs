using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inheritance
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private int BulletDamage;
        [SerializeField] private float BulletSpeed;
        [SerializeField] private int TimeLimit;

        [SerializeField] private ParticleSystem explosionParticles;
        [SerializeField] private AudioClip explosionSound;

        private float Timer;

        private void Awake()
        {
            Timer = 0;
        }

        private void Update()
        {
            //move forward at a constant speed
            transform.Translate(Vector3.forward * Time.deltaTime * BulletSpeed);
            //increase the timer
            Timer += Time.deltaTime;
            //destroy rocket if it has been flying for too long
            if (Timer >= TimeLimit)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter(Collider other)
        {

            //spawn explosion particles
            Instantiate(explosionParticles, transform.position, explosionParticles.transform.rotation);
            //play sound effect
            AudioHelper.PlayClip2D(explosionSound, 1);

            //attempt to damage whatever it hits
            IDamageable damageableHit = other.GetComponent<IDamageable>();
            damageableHit?.TakeDamage(BulletDamage);

            //destroy the rocket
            Destroy(gameObject);
        }
    }
}

