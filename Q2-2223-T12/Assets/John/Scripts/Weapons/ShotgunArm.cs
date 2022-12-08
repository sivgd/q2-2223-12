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
        double spread = (Mathf.Sqrt(pelletAmt) % 1 == 0) ? Mathf.Sqrt(pelletAmt) : Mathf.Ceil(Mathf.Sqrt(pelletAmt));
        int pRow, pColumn;

        for (int i = 0; i < pelletAmt; i++)
        {
            

            Vector3 dir = new Vector3(transform.forward.x * rX, transform.forward.y * rY, transform.forward.z * rZ);
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
