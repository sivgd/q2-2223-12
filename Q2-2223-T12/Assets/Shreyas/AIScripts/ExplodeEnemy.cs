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
    [HideInInspector]
    public float timeBetweenAttack;
    [HideInInspector]
    public bool alreadyAttacked;

    [Header("Range")]
    public float sightRange, AttackRange;
    public bool playerSightInRange, playerInAttackRange;

    [Header("Explode")]
    public GameObject exp;
    public GameObject explosionTrigger, animObject, rendererObject;

    [Header("Look")]
    public float turnSpeed;
    Quaternion rotGoal;
    Vector3 direction;

    public ExplodingEnemyHealth health;
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

        if (!playerInAttackRange)
        {
            Chase();
        }

        if (playerInAttackRange && playerSightInRange)
        {
            Attack();
        }
    }

    private void Chase()
    {
        direction = (player.position - transform.position).normalized;
        rotGoal = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotGoal, turnSpeed);
        agent.SetDestination(player.position);
    }

    private void Attack()
    {
        agent.SetDestination(transform.position);

        direction = (player.position - transform.position).normalized;
        rotGoal = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotGoal, turnSpeed);

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
        health.currentHealth--;
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
