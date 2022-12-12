using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBall : MonoBehaviour
{
    private static float shootForce = 10000f;
    private static float damage = 1f;
    private static float explosionRadius = 12f;
    private static float explosionMult = 100f; 
    private Rigidbody rb; 
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * shootForce * Time.deltaTime,ForceMode.Impulse); 
    }


    private float calcExplosionRadius()
    {
        float output = explosionRadius + damage;
        return output;
    }
    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, calcExplosionRadius()); 
        foreach(Collider hit in colliders)
        {
            Rigidbody r = hit.GetComponent<Rigidbody>();
            if(r != null)
            {
                r.AddExplosionForce(calcExplosionRadius() * explosionMult * Time.deltaTime, transform.position, calcExplosionRadius(),1f,ForceMode.Impulse);
                //ApplyDamage(r.gameObject,damage);
            }
           
        }
    }
    private void ApplyDamage(GameObject enemy,float damage)
    {
        if(enemy.tag != null )
        {
            if (enemy.CompareTag("Enemy"))
            {
                /// Damage enemy
            }
        }
        return; 
    }

    private void OnCollisionEnter(Collision collision)
    {
        Explode();
        Destroy(gameObject); 
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
