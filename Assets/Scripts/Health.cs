using UnityEngine;

public class Health : MonoBehaviour, IDamageable
    {
        [SerializeField] int HP = 30;
        [SerializeField] int MaxHP = 30;

        [SerializeField] public AudioSource deathSFX;
        [SerializeField] public ParticleSystem deathVFX;

        private AudioSource deathAudio;
        private ParticleSystem deathExplode;


    public void TakeDamage(int damage)
        {
            HP -= damage;
            Debug.Log("health: " + HP);
            if (HP <= 0)
            {
                Kill();
            }
        }

        public void Kill()
        {
        deathAudio = Instantiate(deathSFX, transform.position, transform.rotation);
        deathAudio.Play();
        Destroy(deathAudio, deathAudio.clip.length);

        deathExplode = Instantiate(deathVFX, transform.position, transform.rotation);
        deathExplode.Play();
        Destroy(deathExplode, 1);

        gameObject.SetActive(false);
        }

        public int getHP()
        {
            return HP;
        }

        public int getMaxHP()
        {
            return MaxHP;
        }
    }
