using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunParticles : MonoBehaviour
{
    private float rangeOfTravel = 1f;
    public float lifeTime = 1f; 
    public float spread = 0.1f; 
    public float pelletSpeed = 0.3f;
    private bool allowForPelletTravel;
    private Vector3 spreadVector;
    private Rigidbody rb; 
    
    private void Awake()
    {
        rb = GetComponent <Rigidbody>(); 
        float dX, dY, dZ;
        dX = Random.Range(-spread, spread);
        dY = Random.Range(-spread, spread);
        dZ = Random.Range(-spread, spread);
        spreadVector = new Vector3(dX, dY, dZ); 
        allowForPelletTravel = true;
        transform.rotation = Quaternion.LookRotation(transform.forward + spreadVector);
        StartCoroutine(destroyAfterTime()); 
    }

    private void Update()
    {
        if (allowForPelletTravel)
        {
            pelletTravel();
        }
    }
    void pelletTravel()
    {
        rb.velocity = transform.forward * pelletSpeed * Time.deltaTime; 
    }
    IEnumerator destroyAfterTime()
    {
        yield return new WaitForSecondsRealtime(lifeTime);
        Destroy(gameObject); 
    }
}
