using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(TankController))]
public class Player : MonoBehaviour
{
   [SerializeField] int _maxHealth = 3;
    [SerializeField] private GameObject tankBodyPart;
    [SerializeField] private GameObject tankTurretPart;
    [SerializeField] private Material tankBodyMaterial;
    [SerializeField] private Material tankTurretMaterial;
    [SerializeField] private Material invincibilityMaterial;
    int _currentHealth;
    TankController _tankController;
    bool Invincibility;
   
    private void Awake()
    {
        _tankController = GetComponent<TankController>();
    }

    private void Start()
    {
        _currentHealth = _maxHealth;
        Invincibility = false;
    }

    public void IncreaseHealth(int amount)
    {
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
        Debug.Log("Player's health: " + _currentHealth);
    }

    public void DecreaseHealth(int amount)
    {
        if (Invincibility)
        {
            return;
        }
        _currentHealth -= amount;
        Debug.Log("Player's health: " + _currentHealth);
        if(_currentHealth <= 0)
        {
            Kill();
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


    public void Kill()
    {
        if (Invincibility)
        {
            return;
        }
        gameObject.SetActive(false);
        //Play particles
        //play sounds
    }
}
