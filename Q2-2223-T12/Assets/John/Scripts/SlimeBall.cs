using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBall : MonoBehaviour
{

    [Header("External References")]
    public GameObject explosionSphere;

    private static float shootForce = 50000f;
    private static float explosionRadius = 12f;
    private static float damage = 1f;
    private static float explosionMult = 100f; 
    private Rigidbody rb;

    private void Awake()
    {
        StartCoroutine(ApplyLaunchForce());
        rb = GetComponent<Rigidbody>();
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
        explosionSphere.transform.localScale = new Vector3(calcExplosionRadius(),calcExplosionRadius(),calcExplosionRadius());
       // Instantiate(explosionSphere, transform.position,transform.rotation); 
        foreach(Collider hit in colliders)
        {
            Rigidbody r = hit.GetComponent<Rigidbody>();
            if(r != null)
            {
                r.AddExplosionForce(calcExplosionRadius() * explosionMult * Time.deltaTime, transform.position, calcExplosionRadius(),1f,ForceMode.Impulse);
                ApplyDamage(r.gameObject,damage);
            }
           
        }
    }
    private void Explode(int superExplodeMult)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, calcExplosionRadius());
        explosionSphere.transform.localScale = new Vector3(calcExplosionRadius(), calcExplosionRadius(), calcExplosionRadius());
       // Instantiate(explosionSphere, transform.position, transform.rotation);
        foreach (Collider hit in colliders)
        {
            Rigidbody r = hit.GetComponent<Rigidbody>();
            if (r != null)
            {
                r.AddExplosionForce(calcExplosionRadius() * (explosionMult * superExplodeMult) * Time.deltaTime, transform.position, calcExplosionRadius(), 1f, ForceMode.Impulse);
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

    private void OnCollisionEnter(Collision collision)
    {
        /// if you are good at the game you can make a super explosion 
        if (collision.collider.CompareTag("Lily"))
        {
            Explode(3);
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
