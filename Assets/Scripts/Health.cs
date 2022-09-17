using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    public interface IDamageable
    {
        void TakeDamage(int damage);
    }

    public class Health : MonoBehaviour, IDamageable
    {
        [SerializeField] int HP = 30;
        [SerializeField] int MaxHP = 30;


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
            Destroy(gameObject);
        }

        public int getHealth()
        {
            return HP;
        }

        public int getMaxHealth()
        {
            return MaxHP;
        }
    }
