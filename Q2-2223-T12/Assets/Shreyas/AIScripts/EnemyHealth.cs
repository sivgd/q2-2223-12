using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;

    [Header("External References")]
    public EnemyStyle style; 
    void Start()
    {
        currentHealth = maxHealth;
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HurtEnemy(float damage,DamageSource damageSource)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            switch (damageSource)
            {
                case DamageSource.Slimeball:
                    style.broadcastDeath(DeathType.Explosion);
                    break;
                default:
                    style.broadcastDeath(DeathType.Normal);
                    break;
            }
        }
    }
    public void HurtEnemy(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}

