using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEffectManager : MonoBehaviour
{
    [Header("Explosion SFX")]
    public AudioSource explosionWindup;
    public GameObject explosionLight;
    public float explosionScreenShake; 

    private float explosionWindupRatio; 
    private void Update()
    {
        explosionWindupRatio = explosionWindup.time / explosionWindup.clip.length; 
    }
    IEnumerator ExplosionSFX()
    {
        Time.timeScale = 0.1f;
        explosionLight.SetActive(true);
        explosionWindup.Play();
        yield return new WaitUntil(() => explosionWindupRatio >= .55f); 
        Time.timeScale = 1f;
        explosionLight.SetActive(false);
        
    }
    public void CreateExplosionEffect()
    {
        StartCoroutine(ExplosionSFX()); 
    }
    IEnumerator ScreenShake(float shakeAmt, float shakeDuration)
    {
        float dX, dY, dZ; 
        yield return new WaitForEndOfFrame(); 
         

    }
}
