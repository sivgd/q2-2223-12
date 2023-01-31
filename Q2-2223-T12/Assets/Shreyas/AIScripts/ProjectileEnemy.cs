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
    public Animator animator;
    [HideInInspector]
    public float timeBetweenAttack;
    [HideInInspector]
    public bool alreadyAttacked;

    [Header("Range")]
    public float sightRange, AttackRange;
    public bool playerSightInRange, playerInAttackRange;

    [Header("Shoot")]
    public GameObject projectile;
    public GameObject firingPoint;
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
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        playerSightInRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, AttackRange, whatIsPlayer);

        if(!playerInAttackRange)
        {
            Chase();
        }

        if (playerInAttackRange && playerSightInRange && agent.velocity.magnitude == 0)
        {
            Attack();
        }

        animator.SetFloat("Move", agent.velocity.magnitude);
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

        if (!alreadyAttacked)
        {
            StartCoroutine(attackAnim());
        }
    }

    IEnumerator attackAnim()
    {
        animator.SetBool("Attack", true);
        alreadyAttacked = true;
        yield return new WaitForSeconds(0.7f);
        Shoot();
        yield return new WaitForSeconds(0.36f);
        animator.SetBool("Attack", false);
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
