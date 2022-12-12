using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    PlayerMovement player;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        player = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HurtPlayer(int damage)
    {
        currentHealth -= damage;

        player.KnockBack();
    }



}
