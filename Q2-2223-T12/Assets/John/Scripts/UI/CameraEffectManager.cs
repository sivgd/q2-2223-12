using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEffectManager : MonoBehaviour
{
    [Header("General SFX")]
    public Transform initialCameraPosition;
   /* private Vector3 initialStyleUIPosition;
    public bool shakeUI;
    public RectTransform styleUI; */
    [Header("Explosion SFX")]
    public AudioSource explosionWindup;
    public AudioClip[] weaponEffects; 
    public GameObject explosionLight;
    public float explosionScreenShake;
   private bool isShaking = false; 
    private float explosionWindupRatio; 
    [Header("Recoil SFX")]
    public float recoilDuration = 0.3f;
    private Vector3 initialPos;
    /*[Header("Preferences")]
    public float uiShakeMult = 2f; */

   /* private void Start()
    {
        initialStyleUIPosition = styleUI.anchoredPosition3D;

    }*/
    private void Update()
    {
        explosionWindupRatio = explosionWindup.time / explosionWindup.clip.length;
        if (isShaking) ScreenShake(explosionScreenShake);  
    }
    IEnumerator ExplosionSFX()
    {
        Time.timeScale = 0.1f;
        explosionLight.SetActive(true);
        explosionWindup.Play();
        isShaking = true; 
        yield return new WaitUntil(() => explosionWindupRatio >= .55f);
        isShaking = false;
        transform.position = initialCameraPosition.position;
        Time.timeScale = 1f;
        explosionLight.SetActive(false);
    }
    public void CreateExplosionEffect()
    {
        StartCoroutine(ExplosionSFX()); 
    }
    
    public void ScreenShake(float shakeAmt)
    {
        //if (shakeUI) uiShake(shakeAmt);
        float dX, dY, dZ;
        shakeAmt *= 0.5f; // multiplication is faster 
        Vector3 newPosition; 
        transform.position = initialCameraPosition.position;
        dX = transform.position.x + Random.Range(-shakeAmt, shakeAmt); 
        dY = transform.position.y + Random.Range(-shakeAmt, shakeAmt);
        dZ = transform.position.z + Random.Range(-shakeAmt, shakeAmt);
        newPosition = new Vector3(dX, dY, dZ);
        transform.position = newPosition;
    }
    /*public void uiShake(float shakeAmt)
    {
        float dX, dY;
        shakeAmt *= uiShakeMult; 
        styleUI.anchoredPosition3D = initialStyleUIPosition; 
        dX = initialStyleUIPosition.x + Random.Range(-shakeAmt,shakeAmt);
        dY = initialStyleUIPosition.y + Random.Range(-shakeAmt, shakeAmt);

        styleUI.anchoredPosition3D = new Vector2(dX, dY); 
    }*/

    private IEnumerator RecoilSFX(float recoilForce,Transform affectedTransform,Vector3 initialPos)
    { 
        Vector3 offset = affectedTransform.forward * recoilForce;
        affectedTransform.position -= offset; 
        yield return new WaitForSecondsRealtime(recoilDuration);
        affectedTransform.localPosition = initialPos; 
    }
    public void ApplyRecoil(float recoilForce,Transform affectedTransform,Vector3 initialPosition)
    {
        StartCoroutine(RecoilSFX(recoilForce,affectedTransform,initialPosition)); 
    }
    public void playSound(soundEffects soundEffect)
    {
        switch (soundEffect)
        {
            case soundEffects.Shotgun:
                break;
            case soundEffects.Lilypad:
                break;
            case soundEffects.Slimeball:
                break; 
        }
    }
    
    
}
public enum soundEffects
{
    Shotgun,
    Lilypad,
    Slimeball
}
