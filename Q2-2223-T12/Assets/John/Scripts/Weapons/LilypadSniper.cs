using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LilypadSniper : MonoBehaviour
{
    [Header("Shooting Variables")]
    public float recoilAmt = 10f; 
    [Header("External References")]
    public GameObject lilypadPrefab;
    public Transform instPos;
    public CameraEffectManager sfx;
    private Animator animator; 

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
        sfx.ApplyRecoil(recoilAmt,transform,initialPos);
        Instantiate(lilypadPrefab, instPos.position, instPos.rotation);
        animator.SetBool("IsLilyFling", true);
        //transform.localPosition = transform.localPosition;     
    }
   /* IEnumerator WaitForAnimationEnd()
    {
        animator.SetBool("IsLilyFling", true);
        //yield return new WaitUntil(animator.GetBehaviour<>)
    }*/

}
