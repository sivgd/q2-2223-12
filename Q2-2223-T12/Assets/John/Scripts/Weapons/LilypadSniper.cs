using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LilypadSniper : MonoBehaviour
{
    [Header("External References")]
    public GameObject lilypadPrefab;
    public Transform instPos;


    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot(); 
        }
    }

    private void Shoot()
    {
        Instantiate(lilypadPrefab, instPos.position, instPos.rotation);
    }

}
