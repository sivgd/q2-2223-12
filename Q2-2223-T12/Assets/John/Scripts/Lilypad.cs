using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lilypad : MonoBehaviour
{
    
    [Header("Shooting Variables")]
    public float shootForce = 10000f;
    public float damageToGive;
    /// <summary>
    /// The lifetime of the lilypad in seconds 
    /// </summary>
    public float lifeTime = 10f; 

    [Header("External References")]
    public string enemyTag = "Enemy";
    public string enemyTag2 = "ExplodingEnemy";

    private Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * shootForce * Time.deltaTime, ForceMode.Impulse);
        StartCoroutine(LifeTimer());  
    }
    IEnumerator LifeTimer()
    {
        yield return new WaitForSecondsRealtime(lifeTime);
        Destroy(gameObject);
    }

    private void SticktoGameObject(GameObject stickTo,Collider collider)
    {
        rb.velocity = Vector3.zero;
        transform.SetParent(stickTo.transform);
        transform.position = collider.ClosestPointOnBounds(transform.position); 
    }

    private void OnTriggerEnter(Collider collision)
    {
        
        if (collision.CompareTag(enemyTag))
        {
            ApplyDamage(0);
            SticktoGameObject(collision.gameObject,collision); 
        }
        else if(collision.CompareTag(enemyTag2))
        {
            ApplyDamage2(0);
            SticktoGameObject(collision.gameObject, collision);

        }
        else if(collision.name != "Player")
        {
            SticktoGameObject(collision.gameObject,collision); 
        }
    }

    private void ApplyDamage(int amt)
    {
        /// apply damage
        FindObjectOfType<EnemyHealth>().HurtEnemy(damageToGive);
    }
    private void ApplyDamage2(int amt)
    {
        /// apply damage
        FindObjectOfType<ExplodingEnemyHealth>().HurtEnemy(damageToGive);
    }

}
