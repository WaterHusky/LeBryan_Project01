using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
    {
    [SerializeField] private int maxHP;
    [SerializeField] private Collider objectCollider;
    [SerializeField] private List<MeshRenderer> artMeshRenderers;
    [SerializeField] private AudioClip hurtSound;
    [SerializeField] private ParticleSystem killParticles;
    [SerializeField] private AudioClip killSound;
    public int currentHP;
    private void Awake()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(int damage)
    {
        //apply damage
        currentHP -= damage;

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
            r.material.SetColor("_EmissionColor", Color.green);
        }
    }

    private void Kill()
    {
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
}
