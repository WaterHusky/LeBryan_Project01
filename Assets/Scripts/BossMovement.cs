using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{

    bool isHalfHealth = true;

    [SerializeField] float moveSpeed = .2f;
    [SerializeField] float speedIncrease = .5f;
    [SerializeField] Health healthCS;
    public GameObject[] waypoints;
    int current = 0;
    float WPradius = 1;


    private void Update()
    {
        if (healthCS.getHP() <= healthCS.getMaxHP() / 2 && isHalfHealth)
        {
            moveSpeed += speedIncrease;
            isHalfHealth = false;
            Debug.Log("move speed: " + moveSpeed);
        }
        Vector3 relativePos = waypoints[current].transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(relativePos);

        Move();
    }

    public void Move()
    {
        if (Vector3.Distance(waypoints[current].transform.position, transform.position) < WPradius)
        {
            current = Random.Range(0, waypoints.Length);
            if (current >= waypoints.Length)
            {
                current = 0;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, waypoints[current].transform.position, Time.deltaTime * moveSpeed);
    }
}
