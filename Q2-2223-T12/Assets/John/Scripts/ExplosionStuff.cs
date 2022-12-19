using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionStuff : MonoBehaviour
{
    private float scale = 0f;
    float finalRadius;
    float rate;
    private bool canGrow; 
    public void Grow(float finalRadius, float rate)
    {
        this.finalRadius = finalRadius;
        this.rate = rate;
        canGrow = true; 
    }

    private void FixedUpdate()
    {
        if (canGrow)
        {
            float averageScale = (transform.localScale.x + transform.localScale.y + transform.localScale.z) / 3;

            if (averageScale < finalRadius)
            {
                scale += (rate) * Time.fixedDeltaTime;

                transform.localScale = new Vector3(scale, scale, scale);
            }
            else
            {
                Destroy(gameObject); 
            }
        }
    }



}
