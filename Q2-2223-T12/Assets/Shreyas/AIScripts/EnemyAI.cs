using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAI : MonoBehaviour
{
    public EnemyTypeClass enemy;
    public PatrolClass patrol;
    public AttackClass attack;
    public ShootClass fire;
    public RangeClass range;

    [Header("Shoot")]
    public GameObject projectile;
    public GameObject shoot;
    public bool projectileEnemy;

    [Header("Range")]
    public float sightRange, attackRange;
    public bool playerSightInRange, playerInAttackRange;

    private void Awake()
    {
        enemy.player = GameObject.Find("Player").transform;
        enemy.agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        playerSightInRange = Physics.CheckSphere(transform.position, sightRange, enemy.whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, enemy.whatIsPlayer);

        if(!playerSightInRange && !playerInAttackRange)
        {
            Patrol();
        }

        if(playerSightInRange && !playerInAttackRange)
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
        if(!patrol.walkPointSet)
        {
            SearchWalkPoint();
        }

        if(patrol.walkPointSet)
        {
            enemy.agent.SetDestination(patrol.walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - patrol.walkPoint;

        if(distanceToWalkPoint.magnitude < 1)
        {
            patrol.walkPointSet = false;
        }
    }
    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-patrol.walkPointRange, patrol.walkPointRange);
        float randomX = Random.Range(-patrol.walkPointRange, patrol.walkPointRange);

        patrol.walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if(Physics.Raycast(patrol.walkPoint, -transform.up, 2f, enemy.whatIsGround))
        {
            patrol.walkPointSet = true;
        }
    }

    private void Chase()
    {
        enemy.agent.SetDestination(enemy.player.position);
    }

    private void Attack()
    {
        enemy.agent.SetDestination(transform.position);

        transform.LookAt(enemy.player);

        if(!attack.alreadyAttacked)
        {
            Shoot();
            attack.alreadyAttacked = true;
            Invoke(nameof(ResetAttack), attack.timeBetweenAttack);
        }
    }

    private void ResetAttack()
    {
        attack.alreadyAttacked = false;
    }

    private void Shoot()
    {
        Rigidbody rb = Instantiate(projectile, shoot.transform.position, Quaternion.identity).GetComponent<Rigidbody>();

        rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
        rb.AddForce(transform.up * 8f, ForceMode.Impulse);

        Destroy(rb.gameObject, 1f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }


}

[System.Serializable]
public class EnemyTypeClass
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
}

[System.Serializable]
public class PatrolClass
{
    public Vector3 walkPoint;
    [HideInInspector]
    public bool walkPointSet;
    public float walkPointRange;
}

[System.Serializable]
public class AttackClass
{
    public float timeBetweenAttack;
    [HideInInspector]
    public bool alreadyAttacked;
}

[System.Serializable]
public class RangeClass
{

}

[System.Serializable]
public class ShootClass
{

}
