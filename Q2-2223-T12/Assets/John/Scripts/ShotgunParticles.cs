using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunParticles : MonoBehaviour
{
    private float rangeOfTravel = 1f;
    public float spread = 0.1f; 
    public float pelletSpeed = 0.3f;
    private bool allowForPelletTravel;
    private float pointInTravel = 0f; 
    
    private void Awake()
    {
        float dX, dY, dZ;
        dX = Random.Range(-spread, spread);
        dY = Random.Range(-spread, spread);
        dZ = Random.Range(-spread, spread);
        Vector3 spreadVector = new Vector3(dX, dY, dZ);
        Vector3 v = transform.position + (transform.up+ spreadVector); 
        allowForPelletTravel = true;
        transform.rotation = Quaternion.LookRotation(v); 
    }

    private void Update()
    {
        if (allowForPelletTravel)
        {
            pelletTravel();
            pointInTravel += Time.deltaTime * pelletSpeed; 
        }
    }
    void pelletTravel()
    {
        transform.position = Vector3.Lerp(transform.position, transform.position + (-transform.up * rangeOfTravel), pointInTravel);
        if (transform.position.Equals(transform.position + (-transform.up * rangeOfTravel))) Destroy(gameObject); 
    }
}
