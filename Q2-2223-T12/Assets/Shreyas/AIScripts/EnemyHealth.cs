using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;

    
    private EnemyStyle style; 
    void Start()
    {
        currentHealth = maxHealth;
    }
    private void Awake()
    {
        currentHealth = maxHealth; 
        if(gameObject.GetComponent<EnemyStyle>() != null)
        {
            style = gameObject.GetComponent<EnemyStyle>(); 
        }
        else style = gameObject.AddComponent<EnemyStyle>();
       
    }


    public void HurtEnemy(float damage,DamageSource damageSource)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            switch (damageSource)
            {
                case DamageSource.Slimeball:
                    style.broadcastDeath(DeathType.Explosion);
                    break;
                default:
                    style.broadcastDeath(DeathType.Normal);
                    break;
            }
            StartCoroutine(WaitToDie());
        }
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
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }

}

