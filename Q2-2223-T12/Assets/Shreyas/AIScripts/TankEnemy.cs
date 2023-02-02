using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TankEnemy : MonoBehaviour
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
    public float sightRange, shootRange, attackRange;
    public bool playerSightInRange, playerInShootRange, playerInAttackRange;

    [Header("Shoot")]
    public GameObject projectile;
    public GameObject firingPoint;
    public float shootForce, upwardForce;
    public int bulletsPerShot;
    [HideInInspector]
    public int bulletsShot;
    [HideInInspector]
    public bool shooting;

    [Header("Attack")]
    public Animator anim;
    public Animator anim2;
    public GameObject gameAnim1;
    public GameObject gameAnim2;

    [Header("Look")]
    public float turnSpeed;
    Quaternion rotGoal;
    Vector3 direction;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        playerSightInRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInShootRange = Physics.CheckSphere(transform.position, shootRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerSightInRange && !playerInShootRange && !playerInAttackRange)
        {
            Patrol();
        }

        if (playerSightInRange && !playerInShootRange && !playerInAttackRange)
        {
            Chase();
        }

        if (playerInShootRange && playerSightInRange && !playerInAttackRange)
        {
            Attack();
        }
        if (playerInShootRange && playerSightInRange && playerInAttackRange)
        {
            Attack2();
        }

        anim.SetFloat("Move", agent.velocity.magnitude);
    }

    private void Patrol()
    {
        anim.SetBool("Attack1", false);
        anim2.SetBool("Attack2", false);
        gameAnim1.SetActive(true);
        gameAnim2.SetActive(false);
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
        anim.SetBool("Attack1", false);
        anim2.SetBool("Attack2", false);
        gameAnim1.SetActive(true);
        gameAnim2.SetActive(false);
        agent.SetDestination(player.position);
    }

    private void Attack()
    {
        gameAnim1.SetActive(true);
        gameAnim2.SetActive(false);
        anim2.SetBool("Attack2", false);
        firingPoint.SetActive(true);
        agent.SetDestination(transform.position);

        direction = (player.position - transform.position).normalized;
        rotGoal = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotGoal, turnSpeed);

        if (!alreadyAttacked)
        {
            StartCoroutine(Attack1Anim());
        }
    }
    private void Attack2()
    {
        anim.SetBool("Attack1", false);
        agent.SetDestination(transform.position);

        direction = (player.position - transform.position).normalized;
        rotGoal = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotGoal, turnSpeed);

        if (!alreadyAttacked)
        {
            StartCoroutine(Attack2Anim());
        }
    }

    IEnumerator Attack1Anim()
    {
        anim.SetBool("Attack1", true);
        alreadyAttacked = true;
        yield return new WaitForSeconds(3.5f);
        Shoot();
        yield return new WaitForSeconds(0.37f);
        anim.SetBool("Attack1", false); 
        Invoke(nameof(Resetenemy), timeBetweenAttack);
    }

    IEnumerator Attack2Anim()
    {
        gameAnim1.SetActive(false);
        gameAnim2.SetActive(true);
        anim2.SetBool("Attack2", true);
        firingPoint.SetActive(false);
        alreadyAttacked = true;
        yield return new WaitForSeconds(1.1f);
        gameAnim1.SetActive(true);
        gameAnim2.SetActive(false);
        anim2.SetBool("Attack2", false);
        Invoke(nameof(Resetenemy), timeBetweenAttack);
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
        if (Physics.Raycast(ray, out hit))
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
        Gizmos.DrawWireSphere(transform.position, shootRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRange);

    }


}

