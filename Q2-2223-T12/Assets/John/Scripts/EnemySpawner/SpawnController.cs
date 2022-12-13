using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [Header("Outside References")]
    public Spawn[] spawnLocations;
    //public SpawnTrigger[] spawnTriggers;

    [Header("Wave Modifiers")]
   // public GameObject[] waveEnemies; 
    public int waveNumber = 3;
    public float waveDelay = 0.5f; 
    /// <summary>
    /// Increases the amount of enemies per wave by the designated amount
    /// </summary>
    public int enemyWaveIncrease = 0;


    [Header("Preferences")]
    public bool useTriggers = false;
    public bool spawnEqually = false;

    private int spawnLocationNum = 0; 


   
    /// <summary>
    /// Checks whether or not a spawner is ready to spawn, then activates said spawner. 
    /// </summary>
    /// <param name="sp"> The spawner to check </param>
    /// <returns></returns>
    private bool checkSpawnValidity(Spawn sp)
    {
        return sp.CanSpawn(); 
    }

    private void Update()
    {
        if (checkSpawnValidity(spawnLocations[spawnLocationNum]))
        {
            spawnLocations[spawnLocationNum].StartSpawnRoutine(waveDelay);
            Debug.Log($"spawner no {spawnLocationNum} activated!"); 
        }
        else spawnLocationNum++;
        if (spawnLocationNum >= spawnLocations.Length) spawnLocationNum = 0; 
    }



}
