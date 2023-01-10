using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class SlimballLauncher : MonoBehaviour
{
    [Header("Outside References")]
    public GameObject slimeBall;
    public Transform instPos;
    public TextMeshProUGUI chargeText;
    //public GameObject animSlimeBall; 

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
    public float maxScale = 0.3f;
    public Vector3 initialScale = new Vector3(0.1f, 0.1f, 0.1f); 
    public Color unchargedColor,chargedColor;
    public CameraEffectManager sfx; 
    private Vector3 initialPos;
    private Color currentColor;
    private Renderer ballMat;

    ///@TODO: reset charge when player switches weapons 
    private void Start()
    {
        initialPos = transform.localPosition;
        currentColor = unchargedColor;
        ballMat = GetComponent<Renderer>();
        transform.localScale = initialScale; 
    }
    
    private void Update()
    {
       
        //charges the thingy
        if (Input.GetButton("Fire1") && coolDownTimer <= 0f)
        {
            heldTimer += Time.deltaTime;
            charge += Time.deltaTime;
            SlimeBallShake();
            GrowSlimeball(); 
            Debug.Log($"charge: {charge}"); 
        }
        
        if ((Input.GetButtonUp("Fire1") || heldTimer >= maxTimer) && coolDownTimer <= 0f)
        {
            charge = Mathf.Clamp(charge, 0.1f, chargeMult);
            heldTimer = 0f;
            transform.localPosition = initialPos;
            transform.localScale = initialScale; 
            coolDownTimer = maxCoolDown;
            Shoot();
            charge = 0f;
        }
        UpdateColor(); 
       // coolDownTimer -= Time.deltaTime;
        chargeText.text = $"Charge: {charge}";
    }

    public void Shoot()
    {
        Instantiate(slimeBall, instPos.position, instPos.rotation);
        sb = GameObject.FindGameObjectWithTag("Slimeball").GetComponent<SlimeBall>();
        sb.setShootForce(sb.getShootForce() * charge); 
        sb.setDamage(sb.getDamage() * charge); 
    }
    void SlimeBallShake()
    {
        transform.localPosition = initialPos;
        float currShake = (maxShake / chargeMult) * charge; /// makes the shake dependant on the current charge of the slimeball 
        sfx.ScreenShake(currShake);
        float dX = transform.localPosition.x + Random.Range(-currShake, currShake); 
        float dY = transform.localPosition.y + Random.Range(-currShake, currShake);
        float dZ = transform.localPosition.z + Random.Range(-currShake, currShake);
        transform.localPosition = new Vector3(dX, dY, dZ);
    }
    void GrowSlimeball()
    {
        float newScale = (maxScale / chargeMult) * charge;
        transform.localScale = new Vector3(newScale, newScale, newScale); 
    }
    void UpdateColor()
    {
        
        float dR = ((chargedColor.r - unchargedColor.r) / chargeMult) * charge;
        float dG = ((chargedColor.g - unchargedColor.g) / chargeMult) * charge;
        float dB = ((chargedColor.b - unchargedColor.b) / chargeMult) * charge;
        currentColor = new Color(unchargedColor.r + dR, unchargedColor.g + dG, unchargedColor.b + dB); 
        
        ballMat.material.SetColor("_Color", currentColor);
        ballMat.material.SetColor("_EmissionColor", currentColor);
        DynamicGI.UpdateEnvironment(); 
    }

    #region ACCESSORS AND MUTATORS 

    public float getCharge()
    {
        return charge; 
    }
    public Color getColor()
    {
        return currentColor; 
    }
    public float getMaxCoolDown()
    {
        return maxCoolDown; 
    }
    public float getCoolDownTimer()
    {
        return coolDownTimer;
    }
    public void setCharge(float newCharge)
    {
        charge = newCharge; 
    }
    public void setColor(Color newColor)
    {
        currentColor = newColor; 
    }
    public void resetCharge()
    {
        charge = 0; 
    }
    public void setCoolDownTimer(float newTime)
    {
        coolDownTimer = newTime; 
    }
    #endregion
}
