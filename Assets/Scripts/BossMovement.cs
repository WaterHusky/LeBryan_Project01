using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{

    bool isHalfHealth = true;

    [SerializeField] float moveSpeed = .2f;
    [SerializeField] float speedIncrease = .5f;
    [SerializeField] Health healthCS;


    private void Update()
    {
        if (healthCS.getHP() <= healthCS.getMaxHP() / 2 && isHalfHealth)
        {
            moveSpeed += speedIncrease;
            isHalfHealth = false;
            Debug.Log("move speed: " + moveSpeed);
        }

        Move();
    }

    public void Move()
    {
        
    }
}
