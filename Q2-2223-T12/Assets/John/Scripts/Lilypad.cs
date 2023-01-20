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
    /// <summary>
    /// Allows the lilypad to get stuck in walls and enemies
    /// placing a trigger infront of a wall stops the lilypad from going through 
    /// </summary>
    /// <param name="stickTo"></param> the gameobject to stick to
    /// <param name="collider"></param> the collider of the gameobject 
    private void SticktoGameObject(GameObject stickTo, Collider collider)
    {
        rb.velocity = Vector3.zero;
        transform.SetParent(stickTo.transform);
        Vector3 closestPt = collider.ClosestPointOnBounds(transform.position);
        transform.position = closestPt;
        Debug.Log($"NAME: {name}, closestPt: {closestPt}");
    }


    private void OnTriggerEnter(Collider collision)
    {

        if (collision.CompareTag(enemyTag))
        {
            ApplyDamage(collision.gameObject);
            SticktoGameObject(collision.gameObject, collision);
        }
        else if (collision.CompareTag(enemyTag2))
        {
            ApplyDamage2(collision.gameObject);
            SticktoGameObject(collision.gameObject, collision);

        }
        else if (collision.name != "Player" && !collision.CompareTag("EnemyBullet"))
        {
            SticktoGameObject(collision.gameObject, collision);
        }

    }

    private void ApplyDamage(GameObject hitObj)
    {
        /// apply damage
        hitObj.GetComponent<EnemyHealth>().HurtEnemy(damageToGive,DamageSource.Lilypad);

    }
    private void ApplyDamage2(GameObject hitObj)
    {
        /// apply damage
        hitObj.GetComponent<ExplodingEnemyHealth>().HurtEnemy(damageToGive,DamageSource.Lilypad);
    }
}
