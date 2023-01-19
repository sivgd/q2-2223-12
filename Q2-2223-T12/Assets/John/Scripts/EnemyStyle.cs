using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStyle : MonoBehaviour
{
   
    [Header("Score Values")]
    public int styleGiven = 3;
    //public float 

    [Header("Outside References")]
    public StyleMeterController styleMeter; 

    
    public void broadcastDeath(DeathType deathType)
    {
        styleMeter.filterStyleFromDeathType(deathType, styleGiven); 
    }

}
