using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class ProjectileEnemy : MonoBehaviour
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

    [Header("Shoot")]
    public GameObject projectile;
    public GameObject firingPoint;
    public GameObject head;
    public float shootForce, upwardForce;
    public int bulletsPerShot;
    [HideInInspector]
    public int bulletsShot;
    [HideInInspector]
    public bool shooting;

    [Header("Look")]
    public float turnSpeed;
    Quaternion rotGoal;
    Vector3 direction;

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
        if(!walkPointSet)
        {
            SearchWalkPoint();
        }

        if(walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if(distanceToWalkPoint.magnitude < 1)
        {
            walkPointSet = false;
        }
    }
    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if(Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }

    private void Chase()
    {
        agent.SetDestination(player.position);
    }

    private void Attack()
    {
        agent.SetDestination(transform.position);

        direction = (player.position - transform.position).normalized;
        rotGoal = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotGoal, turnSpeed);

        firingPoint.transform.LookAt(player);
        head.transform.LookAt(player);

        if (!alreadyAttacked)
        {
            Shoot();
            alreadyAttacked = true;
            Invoke(nameof(Resetenemy), timeBetweenAttack);
        }
    }

    private void Resetenemy()
    {
        alreadyAttacked = false;
    }

    private void Shoot()
    {
        Ray ray = new Ray(firingPoint.transform.position, transform.TransformDirection(Vector3.forward));
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * 10);

        Vector3 targetPoint;
        if(Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(75);
        }

        Vector3 directionWithoutSpread = targetPoint - firingPoint.transform.position;

        GameObject currentBullet = Instantiate(projectile, firingPoint.transform.position, Quaternion.identity);
        currentBullet.transform.forward = directionWithoutSpread.normalized;

        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithoutSpread.normalized * shootForce, ForceMode.Impulse);
        currentBullet.GetComponent<Rigidbody>().AddForce(firingPoint.transform.up * upwardForce, ForceMode.Impulse);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AttackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);

    }


}
