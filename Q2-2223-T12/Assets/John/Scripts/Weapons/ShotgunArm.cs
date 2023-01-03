using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunArm : MonoBehaviour
{
    [Header("Shooting Variables")]
    public float shotSpread=0.2f;
    public float spreadMult;
    //float rX, rY, rZ;
    public float range = 100f; 
    public string enemyTag;
    public string enemyTag2;
    
    
    [Header("External References")]
    private RaycastHit hit; 
    public Transform instPos;
    public GameObject muzzleFlash;
    public int damageToGive;



    //[SerializeField] Ray[] pellets;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
            StartCoroutine("MuzzleFlash"); 

        }
       
    }
    IEnumerator MuzzleFlash()
    {
        muzzleFlash.SetActive(true);
        yield return new WaitForSecondsRealtime(0.3f);
        muzzleFlash.SetActive(false); 
    }
    private void Fire()
    {
        GenerateBullets(); 
    }

    /// <summary>
    /// Casts a box at a designated range and origin (range & instpos) towards what the player is aiming at 
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
                if (hit.collider.CompareTag(enemyTag) || hit.collider.CompareTag(enemyTag2))
                {
                    ApplyDamage(hit.collider.gameObject);
                }
                
                if (hit.collider.gameObject.GetComponent<Rigidbody>() != null)
                {
                    PushEnemy(hit.collider.gameObject.GetComponent<Rigidbody>(), hit);
                }
            }
        } 

   }
    private void PushEnemy(Rigidbody affectedRB,RaycastHit hit)
    {

       if(affectedRB.CompareTag(enemyTag) || affectedRB.CompareTag(enemyTag2))
       {
            affectedRB.AddExplosionForce(5000f, hit.point, 0.3f);
       }
    }
    private void ApplyDamage(GameObject inputObj)
    {
        if (inputObj.CompareTag(enemyTag))
        {
            //Damage enemy
            Debug.Log("Enemy Damaged");
            inputObj.GetComponent<EnemyHealth>().HurtEnemy(damageToGive);
        }
        else if(inputObj.CompareTag(enemyTag2))
        {
            Debug.Log("Explode");
            inputObj.GetComponent<ExplodingEnemyHealth>().HurtEnemy(damageToGive);
        }
    }
}
