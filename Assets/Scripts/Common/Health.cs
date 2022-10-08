using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
    {
    GameController GC;
    public TankController player;
    public int maxHP;
    [SerializeField] private Collider objectCollider;
    [SerializeField] private List<MeshRenderer> artMeshRenderers;
    [SerializeField] private AudioClip hurtSound;
    [SerializeField] private ParticleSystem killParticles;
    [SerializeField] private AudioClip killSound;
    public int currentHP;
    public event Action<int> TookDamage;
    public event Action<int> GainHealth;
    Spawner spawner;
    private void Awake()
    {
        currentHP = maxHP;
    }

    private void Start()
    {
        GC = GameController.main;
    }

    public void TakeDamage(int damage)
    {
        if(player != null) 
        {
            if(player.Invincibility == true)
            {
                return;
            }
        }
        //apply damage
        currentHP -= damage;

        currentHP = currentHP < 0 ? 0 : currentHP;


        //invoke the event for anything listening
        TookDamage?.Invoke(currentHP);

        //play feedback
        StartCoroutine(HurtFlash());
        AudioHelper.PlayClip2D(hurtSound, 1);

        //kill the object if it reaches 0
        if (currentHP <= 0)
        {
            Kill();
        }
    }

    public void Heal(int heal)
    {
        currentHP += heal;
        GainHealth?.Invoke(currentHP);
        currentHP = currentHP > maxHP ? maxHP : currentHP;
    }

    private IEnumerator HurtFlash()
    {
        //set all materials to red
        foreach (MeshRenderer r in artMeshRenderers)
        {
            r.material.SetColor("_EmissionColor", Color.red);
        }

        yield return new WaitForSeconds(0.1f);

        //set all materials back to normal
        foreach (MeshRenderer r in artMeshRenderers)
        {
            r.material.SetColor("_EmissionColor", Color.black);
        }
    }

    private void Kill()
    {
        if (gameObject.CompareTag("Minion"))
        {
            MinionDeath();
        }

        Debug.Log($"{gameObject.name} has died");
        //spawn kill particles
        Instantiate(killParticles, gameObject.transform);

        //turn off art and collider
        objectCollider.enabled = false;
        foreach (MeshRenderer r in artMeshRenderers)
        {
            r.enabled = false;
        }
        //play kill sound
        AudioHelper.PlayClip2D(killSound, 1);


    }
    public void MinionDeath()
    {
        if(spawner == null)
        { spawner = GC.spawner; }

        spawner.enemyCount -= 1;
    }
  
}
