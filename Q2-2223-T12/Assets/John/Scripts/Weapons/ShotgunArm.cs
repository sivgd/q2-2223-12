using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunArm : MonoBehaviour
{
    public float shotSpread=0.2f;
    private float shotTimer;
    public float maxTimer; 
    //float rX, rY, rZ;

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
        /// pellet pos should have fixed spread based on the sqr root of the amt of pellets 
        float spread = Mathf.Ceil(Mathf.Sqrt(pelletAmt)); 

        int pRow = -1, pColumn = 0;

        for (int i = 0; i < pelletAmt; i++)
        {
           // float xtoZratio = transform.forward.x / transform.forward.z;
            Vector3 dirMod = new Vector3(pRow * spread, -pRow * spread, pColumn * spread);

            Debug.Log($"dirMod: {dirMod.normalized} instPos.forward: {instPos.forward}"); 
            Ray ray = new Ray(instPos.position, instPos.forward + dirMod.normalized);
            Debug.DrawRay(ray.origin,ray.direction,Color.red,0.5f); 
            if (Physics.Raycast(ray, out hit))
            {
                hitObj[i] = hit.collider.gameObject;
            }
            pColumn++; 
            if(pColumn >= spread)
            {
                pRow++;
                pColumn = 0; 
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
