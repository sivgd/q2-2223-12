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

    private Vector3 initialPos;
    private void Start()
    {
        initialPos = transform.localPosition; 
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
        sfx.ApplyRecoil(recoilAmt,transform);
        Instantiate(lilypadPrefab, instPos.position, instPos.rotation);
        transform.localPosition = transform.localPosition
        
    }

}
