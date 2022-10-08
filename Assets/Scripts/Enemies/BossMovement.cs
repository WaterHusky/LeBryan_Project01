using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed;
    [SerializeField] private float leftPatrolBound;
    [SerializeField] private float rightPatrolBound;
    [SerializeField] private float lowerPatrolBound;
    [SerializeField] private float upperPatrolBound;
    [SerializeField] private float patrolLength;


    [Header("Attacks")]
    [SerializeField] private int physicalBossDamage;
    [SerializeField] private List<Transform> BossChargeAttackPositions;
    [SerializeField] private float chargeAttackColliderBox;
    [SerializeField] private float chargeSpeed;

    // movement variables
    private Vector3 targetPatrolPosition;
    private Vector3 targetChargePosition;
    private float patrolTimer;

    //use these as place holders for states that may come later
    private bool patroling;
    private bool reachedPatrolPosition;
    private bool reachedChargePosition;

    // attack variables
    public event Action StartedChargeAttack;
    public event Action EndedChargeAttack;

    private void Awake()
    {
        patroling = true;
        patrolTimer = patrolLength;
        reachedPatrolPosition = true; //start true to generate an initial position
    }

    private void Update()
    {
        if (patroling)
        {
            //walk around to a random spot on the map

            //if the spot has been reached, pick a new spot
            if (reachedPatrolPosition)
            {

                reachedPatrolPosition = false;

                float randX = UnityEngine.Random.Range(leftPatrolBound, rightPatrolBound);
                float randZ = UnityEngine.Random.Range(lowerPatrolBound, upperPatrolBound);
                targetPatrolPosition = new Vector3(randX, transform.position.y, randZ);
            }

            //move towards target position
            transform.LookAt(targetPatrolPosition);
            transform.position = Vector3.MoveTowards(transform.position, targetPatrolPosition, speed * Time.deltaTime);
            //update patroling timer
            patrolTimer -= Time.deltaTime;

            //if the spot has now been reached, set the flag
            if (Vector3.Distance(transform.position, targetPatrolPosition) <= 0.5f)
            {
                reachedPatrolPosition = true;

                //check if the patrol timer is up
                if (patrolTimer <= 0)
                {

                    patroling = false;

                    //pick one of the charge positions
                    int randIndex = UnityEngine.Random.Range(0, BossChargeAttackPositions.Count - 1);
                    targetChargePosition = BossChargeAttackPositions[randIndex].position;

                    reachedChargePosition = false;
                }
            }

        }
        else
        {
            //walk to the randomly selected charge position

            if (!reachedChargePosition)
            {
                //move towards target charging position
                transform.position = Vector3.MoveTowards(transform.position, targetChargePosition, speed * Time.deltaTime);
                transform.LookAt(targetChargePosition);

                //if the spot has now been reached, initiate charge attack
                if (Vector3.Distance(transform.position, targetChargePosition) <= 0.5f)
                {
                    //set flag
                    reachedChargePosition = true;
                    //charge attack
                    StartCoroutine(ChargeAttack());
                }
            }
        }
    }

    private IEnumerator ChargeAttack()
    {
        //invoke event for anything listening
        StartedChargeAttack?.Invoke();

        //change the collider radius to be bigger over time
        BoxCollider collider = GetComponent<BoxCollider>();
        float originalColliderRadius = collider.size.z;
        float totalTime = 2f;
        float timer = 0;
        while (timer < totalTime)
        {
            originalColliderRadius = Mathf.Lerp(originalColliderRadius, chargeAttackColliderBox, timer / totalTime);
            timer += Time.deltaTime;
            GetComponent < Enemy_Boss > ().enabled = false;
            yield return null;
        }

        //move the boss rapidly down on the map
        Vector3 endOfCharge = transform.position + new Vector3(0, 0, -40);
        while (Vector3.Distance(transform.position, endOfCharge) > 0.5)
        {
            transform.position = Vector3.MoveTowards(transform.position, endOfCharge, chargeSpeed * Time.deltaTime);
            yield return null;
        }

        //change the collider radius back to normal over time
        totalTime = 1f;
        timer = 0;
        while (timer < totalTime)
        {
            originalColliderRadius = Mathf.Lerp(chargeAttackColliderBox, originalColliderRadius, timer / totalTime);
            timer += Time.deltaTime;
            yield return null;
        }

        //invoke event for anything listening
        EndedChargeAttack?.Invoke();

        //go back to patroling
        patroling = true;
        patrolTimer = patrolLength;
        reachedPatrolPosition = false;
        GetComponent<Enemy_Boss>().enabled = true;
    }

    private void OnTriggerEnter(Collider collider)
    {
        Health h = collider.gameObject.GetComponent<Health>();
        if(h != null)
        {
            h.TakeDamage(physicalBossDamage);
        }
    }

}
