using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionStuff : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(DestroyAfterTime()); 
    }
    IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSecondsRealtime(5f);
        Destroy(gameObject); 
    }
}
