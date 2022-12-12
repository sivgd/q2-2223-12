using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunArm : MonoBehaviour
{
    public float shotSpread=0.2f;
    public float spreadMult;
    //float rX, rY, rZ;
    public float range = 100f; 
    public string enemyTag;
   
    

    private RaycastHit hit; 
    public Transform instPos;




    //[SerializeField] Ray[] pellets;

    
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

    /// <summary>
    /// Casts a box at a designated range and origin (range & instpos) towards what the player is aimning at 
    /// The range can be changed in the editor
    /// There is also a spread that determines the width and height of the box, it can be modified to increase the amount of enemies hit on screen 
    /// </summary>
   private void GenerateBullets()
   {

        Ray boxRay = new Ray(instPos.position, instPos.forward);
        Vector3 boxRange = new Vector3(shotSpread,shotSpread/3,shotSpread);
        Quaternion boxOrientation = instPos.rotation;
        Debug.DrawRay(boxRay.origin, boxRay.direction,Color.red);
        if (Physics.BoxCast(boxRay.origin, boxRange, boxRay.direction, out hit, boxOrientation, range))
        {
            Debug.Log($"{hit.collider.name} was hit"); 
            if(hit.collider.tag != null)
            {
                if (hit.collider.CompareTag(enemyTag))
                {
                    ApplyDamage(hit.collider.gameObject);

                }
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
