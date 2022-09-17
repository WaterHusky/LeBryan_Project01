using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    [SerializeField] public int _damageAmount = 1;
    [SerializeField] ParticleSystem _impactParticles;
    [SerializeField] AudioClip _impactSound;

    TankController _tankController;
    Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {

    }

    private void OnCollisionEnter(Collision other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if(player != null)
        {
            PlayerImpact(player);
            ImpactFeedback();
        }
        if (_tankController.Invincible == true)
        {
            _tankController.Invincible = false;
        }
    }

    protected virtual void PlayerImpact(Player player)
    {
            player.DecreaseHealth(_damageAmount);
    }

    private void ImpactFeedback()
    {
        // particles
        if(_impactParticles != null)
        {
            _impactParticles = Instantiate(_impactParticles,
                transform.position, Quaternion.identity);
        }
        // audio. TODO - consider Object Pooling for performance
        if(_impactSound != null)
        {
            AudioHelper.PlayClip2D(_impactSound, 1f);
        }
    }

}
