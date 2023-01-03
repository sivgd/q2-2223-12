using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEffectManager : MonoBehaviour
{
    [Header("General SFX")]
    public Transform initialCameraPosition; 
    [Header("Explosion SFX")]
    public AudioSource explosionWindup;
    public GameObject explosionLight;
    public float explosionScreenShake;
    private bool isShaking = false; 
    private float explosionWindupRatio; 
    [Header("Recoil SFX")]
    public float recoilDuration = 0.3f;
   
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
    private IEnumerator RecoilSFX(float recoilForce,Transform affectedTransform)
    {
        /// @TODO: need reference to initial pos
        Vector3 offset = (affectedTransform.forward * recoilForce) + (affectedTransform.right * recoilForce * 0.2f);
        affectedTransform.localPosition -= offset; 
        yield return new WaitForSecondsRealtime(recoilDuration);
       // affectedTransform
    }
    public void ApplyRecoil(float recoilForce,Transform affectedTransform)
    {
        StartCoroutine(RecoilSFX(recoilForce,affectedTransform)); 
    }
}
