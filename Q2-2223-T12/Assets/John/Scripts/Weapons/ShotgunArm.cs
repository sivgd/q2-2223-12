using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunArm : MonoBehaviour
{
    public float shotSpread=0.2f;
    private float shotTimer;
    public float maxTimer;
    public float spreadMult;
    //float rX, rY, rZ;
    public float range = 100f; 
    public int pelletAmt = 5;

    public string enemyTag;

    

    private RaycastHit hit; 
    public Transform instPos;

    


    //[SerializeField] Ray[] pellets;

    private void Start()
    {
       
    }
    


    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Fire(); 
        }  
    }
    private void Fire()
    {
        GenerateBullets(); 
    }

   private void GenerateBullets()
   {
        Ray boxRay = new Ray(instPos.position, instPos.forward);
        Vector3 boxRange = new Vector3(shotSpread,shotSpread,shotSpread);
        Quaternion boxOrientation = instPos.rotation;
        Debug.DrawRay(boxRay.origin, boxRay.direction,Color.red);
        if (Physics.BoxCast(boxRay.origin, boxRange, boxRay.direction, out hit, boxOrientation, range))
        {
            Debug.Log($"{hit.collider.name} was hit"); 
            if (hit.collider.CompareTag(enemyTag))
            {
                ApplyDamage(hit.collider.gameObject); 

            }
        } 

   }

    private void ApplyDamage(GameObject inputObj)
    {
        if (inputObj.CompareTag(enemyTag))
        {
            //Damage enemy
            Debug.Log("Enemy Damaged");
        }
    }
}
