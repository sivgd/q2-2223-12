using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;

    public float timeBeforeHeal;
    private bool healingPlayer;
    public float healthRegen;

    playerMove player;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        player = FindObjectOfType<playerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if(healingPlayer == true)
        {
            currentHealth += healthRegen;
            if(currentHealth >= maxHealth)
            {
                currentHealth = 100;
                healingPlayer = false;
            }
        }
    }

    public void HurtPlayer(int damage)
    {
        currentHealth -= damage;
        healingPlayer = false;
        StopCoroutine("healPlayer");
        StartCoroutine("healPlayer");
    }

    public void explodeHurt(int explodeDamage, Vector3 direction)
    {
        currentHealth -= explodeDamage;

        player.KnockBack(direction);

        healingPlayer = false;
        StopCoroutine("healPlayer");
        StartCoroutine("healPlayer");
    }
    public void tankHurt(int tankDamage)
    {
        currentHealth -= tankDamage;

        player.tankKnockback();

        healingPlayer = false;
        StopCoroutine("healPlayer");
        StartCoroutine("healPlayer");
    }


    IEnumerator healPlayer()
    {
        yield return new WaitForSeconds(timeBeforeHeal);
        healingPlayer = true;
    }


}
