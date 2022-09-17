using UnityEngine;

public class Health : MonoBehaviour, IDamageable
    {
        [SerializeField] public int HP = 30;
        [SerializeField] public int MaxHP = 30;

        [SerializeField] public AudioSource deathSFX;
        [SerializeField] public ParticleSystem deathVFX;

        private AudioSource deathAudio;
        private ParticleSystem deathExplode;

         Player player;


    public void TakeDamage(int damage)
        {
        if (player.Invincibility)
        {
            return;
        }
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
