using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FieldofView : MonoBehaviour
{
    [SerializeField] public float radius;
    [SerializeField] public float angle;

    [SerializeField] public GameObject PlayerTank;

    [SerializeField] public LayerMask TargetSight;
    [SerializeField] public LayerMask ObstructionSight;

    [SerializeField] public NavMeshAgent agent;

    private bool Aggro;

    private float distance;



    private void Start()
    {
        PlayerTank = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        FieldOfViewCheck();
        MoveToPlayer();
    }


    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, TargetSight);
        float angleLower = transform.forward.y - (angle / 2);
        float angleHigher = transform.forward.y + (angle / 2);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angleHigher
                    && Vector3.Angle(transform.forward, directionToTarget) > angleLower)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, ObstructionSight))
                    Aggro = true;
                else
                    Aggro = false;
            }
            else
                Aggro = false;

        }
        else if (Aggro)
            Aggro = false;
    }

    private void MoveToPlayer()
    {
        distance = Vector3.Distance(PlayerTank.transform.position, this.transform.position);

        if (Aggro && distance > 4)
        {
            agent.isStopped = false;
            agent.SetDestination(PlayerTank.transform.position);
        }
        else
            agent.isStopped = true;
    }
}
