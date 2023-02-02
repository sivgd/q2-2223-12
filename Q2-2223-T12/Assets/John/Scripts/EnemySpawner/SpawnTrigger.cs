using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
   
    private bool activated;
    [Header("Outside References")]
    public string playerLayer = "player";
    public GameObject tutorial;
  

    public bool getActivated()
    {
        return activated; 
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            activated = true;
            tutorial.SetActive(false);
            Debug.Log($"{name} triggered!"); 
        }
        Debug.Log($"Collided with {collision.gameObject.name}"); 
       
    }
}
