using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour
{
    public bool tankEnemy;
    public bool explodeEnemy;

    public int damageToGive;
    public int explodeDamage;
    public int tankDamage;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (explodeEnemy == true)
            {
                Vector3 hitDirection = col.transform.position - transform.position;
                hitDirection = hitDirection.normalized;

                FindObjectOfType<HealthManager>().explodeHurt(explodeDamage, hitDirection);
            }
            else if (tankEnemy == true)
            {
                FindObjectOfType<HealthManager>().tankHurt(tankDamage);
            }
            else
            {
                FindObjectOfType<HealthManager>().HurtPlayer(damageToGive);
            }
        }
    }
}
