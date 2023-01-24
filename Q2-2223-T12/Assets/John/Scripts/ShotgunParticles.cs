using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunParticles : MonoBehaviour
{
    private float rangeOfTravel = 1f;
    public float spread = 0.1f; 
    public float pelletSpeed = 0.3f;
    private bool allowForPelletTravel;
    private Vector3 spreadVector; 
    private float pointInTravel = 0f; 
    
    private void Awake()
    {
        float dX, dY, dZ;
        dX = Random.Range(-spread, spread);
        dY = Random.Range(-spread, spread);
        dZ = Random.Range(-spread, spread);
        spreadVector = new Vector3(0f, dY, dZ); 
        allowForPelletTravel = true;
        transform.rotation = Quaternion.LookRotation(transform.up.normalized + spreadVector); 
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
        transform.localPosition = Vector3.Lerp(transform.localPosition, transform.localPosition + (transform.up.normalized * rangeOfTravel),pointInTravel );
        if (transform.position.Equals(transform.position + (transform.up * rangeOfTravel))) Destroy(gameObject); 
    }
}
