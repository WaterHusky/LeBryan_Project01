using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inheritance
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float bulletLifetime = 5.0f;
        [SerializeField] int bulletDamage = 2;
        [SerializeField] float bulletForce = 25.0f;

        [Header("Effects")]
        [SerializeField] AudioClip DamageSound;
        [SerializeField] GameObject DamageParticles;

        [SerializeField] AudioClip KillSound;
        [SerializeField] GameObject KillParticles;

        private void Start()
        {
            StartCoroutine(DestoryBulletAfterTime(gameObject, bulletLifetime));
        }
        private void Update()
        {
            //move forward at a constant speed
            transform.Translate(Vector3.forward * Time.deltaTime * bulletForce);
   
        }

            private void OnTriggerEnter(Collider other)
        {
            // if collider has health
            if (other.gameObject.GetComponent<IDamageable>() != null)
            {
                Health healthScript = other.gameObject.GetComponent<Health>();
                healthScript.TakeDamage(bulletDamage);

                // When health at 0 HP commence death sequence
                if (healthScript.getHP() <= 0)
                {
                    if (KillParticles != null)
                    {
                        GameObject tempKillParticles = Instantiate(KillParticles, gameObject.transform.position, gameObject.transform.rotation);
                        Destroy(tempKillParticles, 2f);
                    }
                    if (KillSound != null)
                    {
                        AudioHelper.PlayClip2D(KillSound, 1f);
                    }
                    Destroy(other.gameObject);
                    Destroy(gameObject);
                }
                // damage effects when still have HP
                else
                {
                    if (DamageParticles != null)
                    {
                        GameObject tempDamageParticles = Instantiate(DamageParticles, gameObject.transform.position, gameObject.transform.rotation);
                        Destroy(tempDamageParticles, 2f);
                    }
                    if (DamageSound != null)
                    {
                        AudioHelper.PlayClip2D(DamageSound, 1f);
                    }
                    Destroy(gameObject);
                }
            }
            else // if collider has no Health - instant kill objects
            {
                Debug.Log("Can't be hurt");

                if (KillParticles != null)
                {
                    GameObject tempKillParticles = Instantiate(KillParticles, gameObject.transform.position, gameObject.transform.rotation);
                    Destroy(tempKillParticles, 2f);
                }
                if (KillSound != null)
                {
                    AudioHelper.PlayClip2D(KillSound, 1f);
                }
                Destroy(other.gameObject);
                Destroy(gameObject);
            }
        }

        private IEnumerator DestoryBulletAfterTime(GameObject bullet, float lifetime)
        {
            yield return new WaitForSeconds(lifetime);

            Destroy(bullet);
        }
    }
}

