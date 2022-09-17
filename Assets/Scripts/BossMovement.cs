using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    Vector3 maxLeftPos = new(-26.2f, 1.48f, 14);
    Vector3 maxRightPos = new(26.5f, 1.48f, 14);

    bool isHalfHealth = true;

    [SerializeField] float moveSpeed = .2f;
    [SerializeField] float speedIncrease = .5f;
    [SerializeField] Health healthCS;


    private void Update()
    {
        if (healthCS.getHealth() <= healthCS.getMaxHealth() / 2 && isHalfHealth)
        {
            moveSpeed += speedIncrease;
            isHalfHealth = false;
            Debug.Log("move speed: " + moveSpeed);
        }

        Move();
    }

    public void Move()
    {
        transform.position = Vector3.Lerp(maxLeftPos, maxRightPos, Mathf.PingPong(Time.time * moveSpeed, 1.0f));
    }
}
