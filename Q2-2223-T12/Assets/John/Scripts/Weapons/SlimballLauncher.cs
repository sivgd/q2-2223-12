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
    public float maxTimer = 2f;
    public float chargeMult = 3f; 

    private float heldTimer = 0f;
    private float coolDownTimer = 0f;
    private float charge = 0f;

    private SlimeBall sb;

 
    private void Start()
    {
       
    }
    private void Update()
    {
       
        //charges the thingy
        if (Input.GetButton("Fire1") && coolDownTimer <= 0f)
        {
            heldTimer += Time.deltaTime;
            charge += Time.deltaTime;
            Debug.Log($"charge: {charge}"); 
        }
        
        if ((Input.GetButtonUp("Fire1") || heldTimer >= maxTimer) && coolDownTimer <= 0f)
        {
            charge = Mathf.Clamp(charge, 0.1f, chargeMult);
            heldTimer = 0f;
            coolDownTimer = maxCoolDown;
            Shoot();
            charge = 0f;
        }
        
        coolDownTimer -= Time.deltaTime; 
    }

    public void Shoot()
    {
        Instantiate(slimeBall, instPos.position, instPos.rotation);
        sb = GameObject.FindGameObjectWithTag("Slimeball").GetComponent<SlimeBall>();
        sb.setDamage(sb.getDamage() * charge); 
        
        
    }

}
