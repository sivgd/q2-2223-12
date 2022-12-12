using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAI : MonoBehaviour
{
    [Header("EnemyType")]
    public bool projectileEnemy;
    public bool explodeEnemy;

    public EnemyClass enemy;
    public RangeClass range;
    public ShootClass projectileEnemyShoot;
    public ExplosionClass explodingEnemy;

    PlayerMovement playerMoves;
    Vector3 direction;

    private void Awake()
    {
        playerMoves = GetComponent<PlayerMovement>();
        enemy.player = GameObject.Find("Player").transform;
        enemy.agent = GetComponent<NavMeshAgent>();
        enemy.alreadyAttacked = false;

    }

    private void Update()
    {
        range.playerSightInRange = Physics.CheckSphere(transform.position, range.sightRange, enemy.whatIsPlayer);
        range.playerInAttackRange = Physics.CheckSphere(transform.position, range.AttackRange, enemy.whatIsPlayer);

        if (!range.playerSightInRange && !range.playerInAttackRange)
        {
            Patrol();
        }

        if(range.playerSightInRange && !range.playerInAttackRange)
        {
            Chase();
        }

        if (range.playerInAttackRange && range.playerSightInRange )
        {
            Attack();
        }
    }

    private void Patrol()
    {
        if(!enemy.walkPointSet)
        {
            SearchWalkPoint();
        }

        if(enemy.walkPointSet)
        {
            enemy.agent.SetDestination(enemy.walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - enemy.walkPoint;

        if(distanceToWalkPoint.magnitude < 1)
        {
            enemy.walkPointSet = false;
        }
    }
    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-enemy.walkPointRange, enemy.walkPointRange);
        float randomX = Random.Range(-enemy.walkPointRange, enemy.walkPointRange);

        enemy.walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if(Physics.Raycast(enemy.walkPoint, -transform.up, 2f, enemy.whatIsGround))
        {
            enemy.walkPointSet = true;
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

        if(!enemy.alreadyAttacked)
        {
            if(projectileEnemy == true)
            {
                Shoot();
            }
            if(explodeEnemy == true)
            {
                Explode();
            }
            enemy.alreadyAttacked = true;
            Invoke(nameof(Resetenemy), enemy.timeBetweenAttack);
        }
    }

    private void Resetenemy()
    {
        enemy.alreadyAttacked = false;
    }

    private void Shoot()
    {
        Ray ray = new Ray(projectileEnemyShoot.firingPoint.transform.position, transform.TransformDirection(Vector3.forward));
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

        Vector3 directionWithoutSpread = targetPoint - projectileEnemyShoot.firingPoint.transform.position;

        GameObject currentBullet = Instantiate(projectileEnemyShoot.projectile, projectileEnemyShoot.firingPoint.transform.position, Quaternion.identity);
        currentBullet.transform.forward = directionWithoutSpread.normalized;

        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithoutSpread.normalized * projectileEnemyShoot.shootForce, ForceMode.Impulse);
        currentBullet.GetComponent<Rigidbody>().AddForce(projectileEnemyShoot.firingPoint.transform.up * projectileEnemyShoot.upwardForce, ForceMode.Impulse);
    }

    private void Explode()
    {
        StartCoroutine(WaitToExplode());
    }
    IEnumerator WaitToExplode()
    {
        yield return new WaitForSeconds(3);
        GameObject exp = Instantiate(explodingEnemy.exp, transform.position, transform.rotation);
        Destroy(exp, 0.5f);
        KnockBack();
        Destroy(gameObject);
    }
    private void KnockBack()
    {

        Collider[] playerCol = Physics.OverlapSphere(transform.position, range.AttackRange);

        foreach (Collider near in playerCol)
        {
            Rigidbody rb = near.GetComponent<Rigidbody>();
            if(rb != null)
            {
                rb.AddExplosionForce(explodingEnemy.knockbackForce, transform.position, range.AttackRange);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range.AttackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range.sightRange);

    }


}

[System.Serializable]
public class EnemyClass
{
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
}

[System.Serializable]
public class RangeClass
{
    public float sightRange, AttackRange;
    public bool playerSightInRange, playerInAttackRange;
}

[System.Serializable]
public class ShootClass
{
    public GameObject projectile;
    public GameObject firingPoint;

    public float shootForce, upwardForce;

    public int bulletsPerShot;

    [HideInInspector]
    public int bulletsShot;
    [HideInInspector]
    public bool shooting;
}

[System.Serializable]
public class ExplosionClass
{
    public GameObject exp;
    public float knockbackForce;
    public float knockBackTime;

    [HideInInspector]
    public float knockBackCounter;

}

