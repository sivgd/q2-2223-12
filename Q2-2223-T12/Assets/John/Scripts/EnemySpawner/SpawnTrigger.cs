using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    private bool activated;
    public string playerLayer = "player"; 

    public bool getActivated()
    {
        return activated; 
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            activated = true;
            Debug.Log($"{name} triggered!"); 
        }
        Debug.Log($"Collided with {collision.gameObject.name}"); 
    }
}
