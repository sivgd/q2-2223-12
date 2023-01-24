using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunArm : MonoBehaviour
{
    [Header("Shooting Variables")]
   /* public float shotSpread=0.2f;
    public float spreadMult;*/
    public float shotDelay = 0.3f; 
    //float rX, rY, rZ;
  //  public float range = 100f;
    public float recoil = 3f; 
    public string enemyTag;
    public string enemyTag2;

    private Vector3 initialPos;
    private bool canFire = true;

    [Header("Particle Settings")]
    public bool hasParticles = true; 
    public int particleAmount = 5;
    public GameObject particle;
    public Transform particleSpawnPoint; 

    [Header("External References")]
    private RaycastHit hit; 
    public Transform instPos;
    public ReturnEnemies shotgunRangeBox; 
    //public GameObject muzzleFlash;
    public int damageToGive;
    private CameraEffectManager sfx;
    Animator animator; 


    //[SerializeField] Ray[] pellets;
    private void Start()
    {
        sfx = FindObjectOfType<CameraEffectManager>().GetComponent<CameraEffectManager>();
        initialPos = transform.localPosition;
        animator = GetComponent<Animator>(); 
    }
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
            //StartCoroutine("MuzzleFlash"); 
        }
       
    }
    /*IEnumerator MuzzleFlash()
    {
        muzzleFlash.SetActive(true);
        yield return new WaitForSecondsRealtime(0.1f);
        muzzleFlash.SetActive(false); 
    }*/
    private void Fire()
    {
        if (canFire)
        {
            GenerateBullets();
            if (hasParticles) GenerateParticles(); 
            //sfx.ApplyRecoil(recoil, transform, initialPos);
            StartCoroutine(Delay(shotDelay));
        }

    }
    /// <summary>
    /// Shot Delay
    /// </summary>
    /// <param name="shotDelay"></param>
    /// <returns></returns>
    IEnumerator Delay(float shotDelay)
    {
        animator.SetTrigger("IsShotgunFire"); 
        canFire = false; 
        yield return new WaitForSecondsRealtime(shotDelay);
        canFire = true;
        animator.ResetTrigger("IsShotgunFire"); 

        
    }
    private void GenerateParticles()
    {
        for(int i = 0; i <= particleAmount; i++)
        {
            Instantiate(particle, particleSpawnPoint.position, instPos.rotation);
        } 
    }
    /// <summary>
    /// Casts a box at a designated range and origin (range & instpos) towards what the player is aiming at 
    /// The range can be changed in the editor
    /// There is also a spread that determines the width and height of the box, it can be modified to increase the amount of enemies hit on screen 
    /// </summary>
   private void GenerateBullets()
   {
        shotgunRangeBox.gameObject.SetActive(true);
        Collider[] colHit = shotgunRangeBox.getColliders();  
        
        /*Ray boxRay = new Ray(instPos.position, instPos.forward);
        Vector3 boxRange = new Vector3(shotSpread,shotSpread/3,shotSpread);
        Quaternion boxOrientation = instPos.rotation;
        Debug.DrawRay(boxRay.origin, boxRay.direction * range,Color.red);


       
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
        } */

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
            inputObj.GetComponent<EnemyHealth>().HurtEnemy(damageToGive,DamageSource.Shotgun);
        }
        else if(inputObj.CompareTag(enemyTag2))
        {
            Debug.Log("Explode");
            inputObj.GetComponent<ExplodingEnemyHealth>().HurtEnemy(damageToGive,DamageSource.Shotgun);
        }
    }
}
