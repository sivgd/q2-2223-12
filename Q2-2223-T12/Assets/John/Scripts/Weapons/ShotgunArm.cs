using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunArm : MonoBehaviour
{
    public float shotSpread = 0.5f;
    public int pelletAmt = 5;
    public Transform instPos;
    float rX, rY, rZ;
    private RaycastHit hit; 
    private GameObject[] hitObj;
    public string enemyTag; 
    //[SerializeField] Ray[] pellets;

    private void Start()
    {
        hitObj = new GameObject[pelletAmt]; 
    }

    private void Fire()
    {

    }

    IEnumerator GenerateBullets()
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();

            rX = Random.Range(0f, shotSpread); 
            rY = Random.Range(0f, shotSpread);
            rZ = Random.Range(0f, shotSpread);
            Vector3 dir = new Vector3(transform.forward.x * rX, transform.forward.y * rY, transform.forward.z * rZ);

            for (int i = 0; i < pelletAmt; i++)
            {
                Ray ray = new Ray(instPos.position, dir);
                if (Physics.Raycast(ray, out hit))
                {
                    hitObj[i] = hit.collider.gameObject;
                }
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
