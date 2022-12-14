using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    [SerializeField] float _moveSpeed = .25f;
    [SerializeField] float _turnSpeed = 2f;

    Rigidbody _rb = null;

    [SerializeField] private GameObject turretPivot;
    [SerializeField] private Transform BulletSpawnPoint;
    [SerializeField] private GameObject BulletPrefab;
    [SerializeField] private float BulletCooldown;

    [SerializeField] private ParticleSystem BulletFiringParticles;
    [SerializeField] private AudioClip BulletFiringSound;

    [SerializeField] private GameObject tankBodyPart;
    [SerializeField] private GameObject tankTurretPart;
    [SerializeField] private Material tankBodyMaterial;
    [SerializeField] private Material tankTurretMaterial;
    [SerializeField] private Material invincibilityMaterial;

  
    public bool Invincibility;
    Health health;

    private float bulletTimer;
    public float MaxSpeed
    {
        get => _moveSpeed;
        set => _moveSpeed = value > 0 ? value : 0.1f;
    }

  

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        bulletTimer = 0;

    }

    private void Start()
    {
        Invincibility = false;
    }


    private void FixedUpdate()
    {
        MoveTank();
        TurnTank();
        TurnTurret();
    }

    private void Update()
    {
        bulletTimer -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space) && bulletTimer <= 0)
        {
            SpawnBullet();
        }
    }

    public void InvincibleOn()
    {
        Invincibility = true;
        tankBodyPart.GetComponent<MeshRenderer>().material = invincibilityMaterial;
        tankTurretPart.GetComponent<MeshRenderer>().material = invincibilityMaterial;
    }

    public void InvincibleOff()
    {
        Invincibility = false;
        tankBodyPart.GetComponent<MeshRenderer>().material = tankBodyMaterial;
        tankTurretPart.GetComponent<MeshRenderer>().material = tankTurretMaterial;
    }

    public void DecreaseHealth(int amount)
    {
        if(Invincibility == true)
        {
            return;
        }

        health.TakeDamage(amount);
    }

    public void Kill()
    {
        gameObject.SetActive(false);
    }

    public void MoveTank()
    {
        // calculate the move amount
        float moveAmountThisFrame = Input.GetAxis("Vertical") * _moveSpeed;
        // create a vector from amount and direction
        _rb.velocity = transform.forward * moveAmountThisFrame * Time.fixedDeltaTime * 1800f;

    }

    public void TurnTank()
    {
        // calculate the turn amount
        float turnAmountThisFrame = Input.GetAxis("Horizontal") * _turnSpeed;
        // create a Quaternion from amount and direction (x,y,z)
        Quaternion turnOffset = Quaternion.Euler(0, turnAmountThisFrame, 0);
        // apply quaternion to the rigidbody
        _rb.MoveRotation(_rb.rotation * turnOffset);
    }

    private void TurnTurret()
    {
        //uses raycasts to get the world position the mouse is pointing to
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100))
        {
            //points the turret in the direction of the mouse, only rotating on the x axis
            turretPivot.transform.LookAt(new Vector3(hit.point.x, turretPivot.transform.position.y, hit.point.z));
        }
    }

    private void SpawnBullet()
    {
        //reset countdown timer
        bulletTimer = BulletCooldown;
        //spawn particle effects
        Instantiate(BulletFiringParticles, BulletSpawnPoint);
        //play sound effect
        AudioHelper.PlayClip2D(BulletFiringSound, 1);
        //spawn rocket
        GameObject b = Instantiate(BulletPrefab, BulletSpawnPoint.position, BulletSpawnPoint.rotation);
        b.GetComponent<Projectile>().ownerTag = gameObject.tag;
    }
}
