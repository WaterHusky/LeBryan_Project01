using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUpBase : MonoBehaviour
{
    GameController GC;
    [SerializeField] private float PowerupLength;
    [SerializeField] private ParticleSystem collectParticles;
    [SerializeField] private AudioClip collectSound;
    private TankController player;
    private MeshRenderer Mesh;
    private Collider powerupCollider;
    private bool PowerUpState;
    private float PowerupTime;
    Spawner spawner;

    protected abstract void PowerUp(TankController player);
    protected abstract void PowerDown(TankController player);

    private void Start()
    {
        GC = GameController.main;
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
        if (spawner == null)
        { spawner = GC.spawner; }
        player = other.gameObject.GetComponent<TankController>();
        if (player != null)
        {
            PowerUp(player);
            spawner.powerupCount -= 1;
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
