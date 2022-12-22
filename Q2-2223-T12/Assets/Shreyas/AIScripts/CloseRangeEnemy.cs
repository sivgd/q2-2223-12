using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CloseRangeEnemy : MonoBehaviour
{
    [Header("EnemyMove")]
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    public Vector3 walkPoint;
    [HideInInspector]
    public bool walkPointSet;
    public float walkPointRange;
    public float timeBetweenAttack;
    [HideInInspector]
    public bool alreadyAttacked;

    [Header("Range")]
    public float sightRange, AttackRange;
    public bool playerSightInRange, playerInAttackRange;

    [Header("Attack")]
    public Animator anim;


    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        playerSightInRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, AttackRange, whatIsPlayer);

        if (!playerSightInRange && !playerInAttackRange)
        {
            Patrol();
        }

        if (playerSightInRange && !playerInAttackRange)
        {
            Chase();
        }

        if (playerInAttackRange && playerSightInRange)
        {
            Attack();
        }
    }

    private void Patrol()
    {
        anim.SetBool("Attack", false);
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1)
        {
            walkPointSet = false;
        }
    }
    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }

    private void Chase()
    {
        anim.SetBool("Attack", false);
        agent.SetDestination(player.position);
    }

    private void Attack()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            anim.SetBool("Attack", true);
            alreadyAttacked = true;
            Invoke(nameof(Resetenemy), timeBetweenAttack);
        }
    }

    private void Resetenemy()
    {
        alreadyAttacked = false;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AttackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);

    }


}
