using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBall : MonoBehaviour
{
    [Header("Shooting Variables")]
    public float shootForce = 50000f;
    public float damage = 1f;

    [Header("Explosion variables")]
    public float explosionRadius = 12f;
    public float explosionRadMult = 100f;
    public int superExplosionMult = 3;
    public float explosionGrowthRate = 30f;

    [Header("External References")]
    public GameObject explosionSphere;
    private CameraEffectManager sfx;
    public string enemyTagOne = "Enemy";
    public string enemyTagTwo; 
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        sfx = FindObjectOfType<CameraEffectManager>(); 
        StartCoroutine(ApplyLaunchForce());
    }
    IEnumerator ApplyLaunchForce()
    {
        yield return new WaitForEndOfFrame(); 
        
        rb.AddForce(transform.forward * shootForce * Time.deltaTime, ForceMode.Impulse);
        StopCoroutine(ApplyLaunchForce());
    }

    private float calcExplosionRadius()
    {
        float output = explosionRadius + damage;
        return output;
    }
    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, calcExplosionRadius()); /// Explosion sphere grows 

        GameObject explosion  = Instantiate(explosionSphere, transform.position,transform.rotation);    /// Instantiates an explosion sphere (for visual / feedback purposes only) 

        explosion.SetActive(true);
        explosion.GetComponent<ExplosionStuff>().enabled = true; 
        explosion.GetComponent<ExplosionStuff>().Grow(calcExplosionRadius(), explosionGrowthRate);

        foreach (Collider hit in colliders)  /// Checks each collider hit in the spherecast, and whether they have rigidbodies 
        {
            Rigidbody r = hit.GetComponent<Rigidbody>();
            if(r != null)
            {
                
                r.AddExplosionForce(calcExplosionRadius() * explosionRadMult * Time.deltaTime, transform.position, calcExplosionRadius(),1f,ForceMode.Impulse);
                ApplyDamage(r.gameObject,damage);
            }
           
        }
    }

    private void Explode(int superExplodeMult)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, calcExplosionRadius());

        GameObject explosion = Instantiate(explosionSphere, transform.position, transform.rotation); /// Instantiates an explosion sphere (for visual / feedback purposes only) 

        explosion.SetActive(true);
        explosion.GetComponent<ExplosionStuff>().enabled = true; 
        explosion.GetComponent<ExplosionStuff>().Grow(calcExplosionRadius(), explosionGrowthRate); /// Explosion sphere grows 

        foreach (Collider hit in colliders)  /// Checks each collider hit in the spherecast, and whether they have rigidbodies 
        {
            Rigidbody r = hit.GetComponent<Rigidbody>();
            if (r != null)
            {
               
                r.AddExplosionForce(calcExplosionRadius() * (explosionRadMult * superExplodeMult) * Time.deltaTime, transform.position, calcExplosionRadius(), 1f, ForceMode.Impulse);
                ApplyDamage(r.gameObject,damage);
            }

        }
    }
    private void ApplyDamage(GameObject enemy,float damage)
    {
       
        if(enemy.tag != null )
        {
            if (enemy.CompareTag("Enemy"))
            { 
                enemy.GetComponent<EnemyHealth>().HurtEnemy(damage);
            }
            if (enemy.CompareTag("ExplodingEnemy"))
            {
                enemy.GetComponent<ExplodingEnemyHealth>().HurtEnemy(damage);
            }
        }
        return; 
    }
    /// <summary>
    /// Calculates the damage given by the explosion based on its distance 
    /// </summary>
    /// <param name="distance"></param> the distance of the gameobject from the explosion 
    /// <param name="radius"></param> the radius of the explosion 
    /// <returns></returns>
    /*private float CalculateDamageFromDistance(float distance, float radius)
    {
        float distRatio = radius / distance;
        if (distRatio >= 1) return damage;
        else if (distRatio >= 0.75) return (damage * 0.75f);
        else return 0f; 

    }*/

   
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Lily"))
        {
            Explode(superExplosionMult);
            sfx.CreateExplosionEffect();
            Destroy(other.gameObject);
            Destroy(gameObject);

        }
        else
        {
            Explode();
            Destroy(gameObject);
        }
    }
    #region Accessors and Mutators
    public float getShootForce()
    {
        return shootForce; 
    }
    public void setShootForce(float newVal)
    {
        shootForce = newVal; 
    }
    public float getDamage()
    {
        return damage; 
    }
    public void setDamage(float newVal)
    {
        damage = newVal; 
    }
    #endregion

}
