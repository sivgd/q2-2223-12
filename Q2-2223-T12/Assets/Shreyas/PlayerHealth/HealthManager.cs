using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class HealthManager : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;

    public float timeBeforeHeal;
    private bool healingPlayer;
    private bool isDead; 
    public float healthRegen;
    public GameObject DeathUI;
    public GameObject PauseUI;

    public Volume volume;
    public GameObject urpThing;
    public Vignette vig;

    playerMove player;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        player = FindObjectOfType<playerMove>();
        urpThing = GameObject.Find("PostProcessing");
        volume = urpThing.GetComponent<Volume>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(healingPlayer == true)
        {
            currentHealth += healthRegen * Time.timeScale;
            if(currentHealth >= maxHealth)
            {
                currentHealth = 100;
                healingPlayer = false;
            }
        }

        if(currentHealth < 40 && currentHealth > 20)
        {
            Vignette tmp;
            if(volume.profile.TryGet<Vignette>(out tmp))
            {
                vig = tmp;
                vig.intensity.SetValue(new ClampedFloatParameter(0.5f, 0.5f, 0.5f));
            }
            
        }
        else if(currentHealth <= 20)
        {
            Vignette tmp;
            if (volume.profile.TryGet<Vignette>(out tmp))
            {
                vig = tmp;
                vig.intensity.SetValue(new ClampedFloatParameter(0.7f, 0.7f, 0.7f));
            }
        }
        else
        {
            Vignette tmp;
            if (volume.profile.TryGet<Vignette>(out tmp))
            {
                vig = tmp;
                vig.intensity.SetValue(new ClampedFloatParameter(0f, 0f, 0f));
            }
        }
        if(currentHealth <= 0)
        {
            isDead = true; 
        }

        if (isDead == true)
        {
            DeathUI.SetActive(true);
            //DeathUI.GetComponent<ScoreKeeper>().setGameOver(true);
            PauseUI.SetActive(false);
            Time.timeScale = 0;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            DeathUI.GetComponent<ScoreKeeper>().setGameOver(true);

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
    public bool getDead()
    {
        return isDead; 
    }

}
