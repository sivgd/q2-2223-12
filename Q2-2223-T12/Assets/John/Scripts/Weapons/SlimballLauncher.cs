using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimballLauncher : MonoBehaviour
{
    [Header("Outside References")]
    public GameObject slimeBall;
    public Transform instPos; 

    [Header("Shooting Variables")]
    public float maxCoolDown = 5f;
    public float maxTimer = 5f; 

    private float heldTimer = 0f;
    private float coolDownTimer = 0f; 

    private SlimeBall sb; 

    private void Update()
    {

        //charges the thingy
        if (Input.GetButton("Fire1") && coolDownTimer <= 0f)
        {
            heldTimer += Time.deltaTime;  
        }
        if ((Input.GetButtonUp("Fire1") || heldTimer >= maxTimer) && coolDownTimer <= 0f)
        {
            heldTimer = 0f;
            coolDownTimer = maxCoolDown; 
            Shoot(); 
        }

        coolDownTimer -= Time.deltaTime; 
    }

    public void Shoot()
    {
        Instantiate(slimeBall, instPos.position,instPos.rotation); 
    }

}
