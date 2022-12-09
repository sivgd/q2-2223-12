using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimballLauncher : MonoBehaviour
{
    [Header("Outside References")]
    public GameObject slimeBall;
    public Transform instPos; 

    [Header("Shooting Variables")]
    public float cooldown = 5f;


    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot(); 
        }
    }

    public void Shoot()
    {
        Instantiate(slimeBall, instPos.position,instPos.rotation); 
    }

}
