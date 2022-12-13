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
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer.Equals(playerLayer))
        {
            activated = true; 
        }
    }
}
