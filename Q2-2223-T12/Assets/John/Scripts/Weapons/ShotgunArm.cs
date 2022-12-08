using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunArm : MonoBehaviour
{
    public float shotSpread = 0.2f;
    private float shotTimer;
    public float maxTimer; 
    float rX, rY, rZ;

    public int pelletAmt = 5;

    public string enemyTag;

    private bool canFire; 

    private RaycastHit hit; 
    private GameObject[] hitObj;
    public Transform instPos;

    


    //[SerializeField] Ray[] pellets;

    private void Start()
    {
        hitObj = new GameObject[pelletAmt];
    }
    private void Awake()
    {
        StartCoroutine(GenerateBullets());
    }


    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && canFire)
        {
            canFire = false;
            shotTimer = maxTimer; 
            Fire(); 
        }
        else if (!canFire) shotTimer -= Time.deltaTime;
        if(shotTimer <= 0f)
        {
            canFire = true; 
        }
        
           
        
    }
    private void Fire()
    {
        StartCoroutine(GenerateBullets()); 
    }

    IEnumerator GenerateBullets()
    {
        
            yield return new WaitForSecondsRealtime(shotTimer);

            rX = Random.Range(0f, shotSpread); 
            rY = Random.Range(0f, shotSpread);
            rZ = Random.Range(0f, shotSpread);
            Vector3 dir = new Vector3(transform.forward.x * rX, transform.forward.y * rY, transform.forward.z * rZ);

            for (int i = 0; i < pelletAmt; i++)
            {
                Ray ray = new Ray(instPos.position, dir);
                Debug.DrawRay(ray.origin,ray.direction,Color.red,0.5f); 
                if (Physics.Raycast(ray, out hit))
                {
                    hitObj[i] = hit.collider.gameObject;
                }
            }
        

    }

    private void ApplyDamage(GameObject[] inputObj)
    {
        for(int i = 0; i< inputObj.Length; i++)
        {
            if (inputObj[i].CompareTag(enemyTag)){
                //Damage enemy
                Debug.Log("Enemy Damaged"); 
            }
        }
    }
}
