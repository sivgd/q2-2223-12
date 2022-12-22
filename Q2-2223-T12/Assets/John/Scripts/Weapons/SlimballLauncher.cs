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

    [Header("UI Stuff")]
    public float maxShake = 2f;

    private Vector3 initialPos;

    private void Start()
    {
        initialPos = transform.localPosition; 
    }
    private void Update()
    {
       
        //charges the thingy
        if (Input.GetButton("Fire1") && coolDownTimer <= 0f)
        {
            heldTimer += Time.deltaTime;
            charge += Time.deltaTime;
            StartCoroutine(SlimeBallShake()); 
            Debug.Log($"charge: {charge}"); 
        }
        
        if ((Input.GetButtonUp("Fire1") || heldTimer >= maxTimer) && coolDownTimer <= 0f)
        {
            charge = Mathf.Clamp(charge, 0.1f, chargeMult);
            StopCoroutine(SlimeBallShake());
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
        sb.setShootForce(sb.getShootForce() * charge); 
        sb.setDamage(sb.getDamage() * charge); 
    }
    IEnumerator SlimeBallShake()
    {
        yield return new WaitForEndOfFrame();
        transform.localPosition = initialPos;
        float currShake = (maxShake / chargeMult) * charge; /// makes the shake dependant on the current charge of the slimeball 
        float dX = transform.localPosition.x + Random.Range(-currShake, currShake); 
        float dY = transform.localPosition.y + Random.Range(-currShake, currShake);
        float dZ = transform.localPosition.z + Random.Range(-currShake, currShake);
        transform.localPosition = new Vector3(dX, dY, dZ);
    }
    

}
