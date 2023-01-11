using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LilypadSniper : MonoBehaviour
{
    [Header("Shooting Variables")]
    public float recoilAmt = 10f;
    public float animTime = 3f; 
    [Header("External References")]
    public GameObject lilypadPrefab;
    public Transform instPos;
    public CameraEffectManager sfx;
    private Animator animator; 

    private bool isAnimating = false; 
    private Vector3 initialPos;
    private void Start()
    {
        initialPos = transform.localPosition;
        animator = GetComponent<Animator>(); 
    }
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (!isAnimating)
        {
            //sfx.ApplyRecoil(recoilAmt,transform,initialPos);
            animator.SetTrigger("IsLilyFling");
            isAnimating = true;
            StartCoroutine(WaitForAnimationDone()); 
            Instantiate(lilypadPrefab, instPos.position, instPos.rotation);
            //transform.localPosition = transform.localPosition;     
        }
    }
    IEnumerator WaitForAnimationDone()
    {
        yield return new WaitForSecondsRealtime(animTime - 0.01f);
        isAnimating = false;
    
    }
    public bool GetAnimating()
    {
        return isAnimating; 
    }

}
