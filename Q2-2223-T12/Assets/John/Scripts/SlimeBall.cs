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
        Collider[] colliders = Physics.OverlapSphere(transform.position, calcExplosionRadius());
        //explosionSphere.transform.localScale = new Vector3(calcExplosionRadius(),calcExplosionRadius(),calcExplosionRadius());
        GameObject explosion  = Instantiate(explosionSphere, transform.position,transform.rotation);
        explosion.SetActive(true);
        explosion.GetComponent<ExplosionStuff>().Grow(calcExplosionRadius(), explosionGrowthRate);
        foreach (Collider hit in colliders)
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
        //explosionSphere.transform.localScale = new Vector3(calcExplosionRadius(), calcExplosionRadius(), calcExplosionRadius());
        GameObject explosion = Instantiate(explosionSphere, transform.position, transform.rotation);
        explosion.SetActive(true);
        explosion.GetComponent<ExplosionStuff>().Grow(calcExplosionRadius(), explosionGrowthRate);
        foreach (Collider hit in colliders)
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
                FindObjectOfType<ExplodingEnemyHealth>().HurtEnemy(damage);
            }
        }
        return; 
    }

    private void OnTriggerEnter(Collider collision)
    {
        /// if you are good at the game you can make a super explosion 
        if (collision.CompareTag("Lily"))
        {
            Explode(superExplosionMult);
            sfx.CreateExplosionEffect(); 
            Destroy(collision.gameObject);
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
