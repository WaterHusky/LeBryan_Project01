using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUpBase : MonoBehaviour
{
    [SerializeField] private float PowerupLength;
    [SerializeField] private ParticleSystem collectParticles;
    [SerializeField] private AudioClip collectSound;
    private Player player;
    private MeshRenderer Mesh;
    private Collider powerupCollider;
    private bool PowerUpState;
    private float PowerupTime;

    protected abstract void PowerUp(Player player);
    protected abstract void PowerDown(Player player);

    private void Awake()
    {
        Mesh = GetComponent<MeshRenderer>();
        powerupCollider = GetComponent<Collider>();
        PowerUpState = false;
    }

    private void Update()
    {
        PowerupTime -= Time.deltaTime;
        if (PowerupTime <= 0 && PowerUpState)
        {
            PowerDown(player);
            PowerUpState = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            PowerUp(player);
            PowerupTime = PowerupLength;
            PowerUpState = true;
            Feedback();
        }
    }

    private void Feedback()
    {
        //turn off visuals for powerup
        Mesh.enabled = false;
        powerupCollider.enabled = false;
        //particles for powerup
        if (collectParticles != null)
        {
            collectParticles = Instantiate(collectParticles, transform.position, Quaternion.identity);
        }
        //audio for collecting
        if (collectSound != null)
        {
            AudioHelper.PlayClip2D(collectSound, 1);
        }
    }

    protected void DestroyPowerup()
    {
        gameObject.SetActive(false);
    }
}
