using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ExplodeEnemy : MonoBehaviour
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

    [Header("Explode")]
    public GameObject exp;
    public GameObject explosionTrigger, animObject, rendererObject;

    ExplodingEnemyHealth health;
    // Start is called before the first frame update
    void Start()
    {
        explosionTrigger.SetActive(false);
        animObject.SetActive(false);
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
        transform.LookAt(player);
        agent.SetDestination(player.position);
    }

    private void Attack()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            Explode();
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

    private void Explode()
    {
        StartCoroutine(WaitToExplode());
    }
    IEnumerator WaitToExplode()
    {
        agent.speed = 0;
        rendererObject.SetActive(false);
        animObject.SetActive(true);
        yield return new WaitForSeconds(3);
        GameObject explosion = Instantiate(exp, animObject.transform.position, transform.rotation);
        explosionTrigger.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        Destroy(explosion, 0.5f);
        Destroy(gameObject, 0.1f);
    }

    public void ExplodeImmediatley()
    {
        StartCoroutine(ExplodeNow());
    }

    IEnumerator ExplodeNow()
    {
        agent.speed = 0;
        GameObject explosion = Instantiate(exp, transform.position, transform.rotation);
        explosionTrigger.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        Destroy(explosion, 0.5f);
        Destroy(gameObject, 0.1f);
    }
}
