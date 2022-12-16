using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ExplodingEnemyHealth : MonoBehaviour
{
    public NavMeshAgent agent;
    public float maxHealth;
    public float currentHealth;

    [Header("Explode")]
    public GameObject exp;
    public GameObject explosionTrigger, animObject, rendererObject;

    ExplodeEnemy explode;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HurtEnemy(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            StartCoroutine(WaitToDie());
        }
    }

    IEnumerator WaitToDie()
    {
        agent.speed = 0;
        GameObject explosion = Instantiate(exp, transform.position, transform.rotation);
        explosionTrigger.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        Destroy(explosion, 0.5f);
        Destroy(gameObject, 0.1f);
    }
}
