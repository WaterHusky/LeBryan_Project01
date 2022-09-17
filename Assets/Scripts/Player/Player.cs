using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(TankController))]
public class Player : MonoBehaviour
{

    [SerializeField] private GameObject tankBodyPart;
    [SerializeField] private GameObject tankTurretPart;
    [SerializeField] private Material tankBodyMaterial;
    [SerializeField] private Material tankTurretMaterial;
    [SerializeField] private Material invincibilityMaterial;
    int _currentHealth;
    TankController _tankController;
    public bool Invincibility;
    Health health;
    Enemy enemy;
   
    private void Awake()
    {
        _tankController = GetComponent<TankController>();
    }

    private void Start()
    {
        Invincibility = false;
    }


    public void DecreaseHealth(int amount)
    {
        health.TakeDamage(enemy._damageAmount);
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

    public void IncreaseHealth(int amount)
    {
        health.HP = Mathf.Clamp(health.HP, 0, health.MaxHP);
        Debug.Log("Player's health: " + health.HP);
    }

    public void Kill()
    {
        health.Kill();
    }
}
