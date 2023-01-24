using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnEnemies : MonoBehaviour
{
    private List<Collider> collidersHit = new List<Collider>();
    private bool active; 
    private void Awake()
    {
        active = true; 
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (active)
        {
            if (!collidersHit.Contains(collision)) collidersHit.Add(collision); 
        }
    }
    public Collider[] getColliders()
    {
        return collidersHit.ToArray(); 
    }
    private void OnDisable()
    {
        collidersHit.Clear();
        active = false; 
    }
}
