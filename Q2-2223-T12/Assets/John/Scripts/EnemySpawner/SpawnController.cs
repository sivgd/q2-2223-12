using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [Header("Outside References")]
    public Spawn[] spawnLocations;
    public GameObject[] levelBarriers;
    public GameObject toEndCollider;
    //public SpawnTrigger[] spawnTriggers;

    [Header("Spawning Modifiers")]
   // public GameObject[] waveEnemies; 
   // public int waveNumber = 3;
    //public float spawnDelay = 0.5f; 
    /// <summary>
    /// Increases the amount of enemies per wave by the designated amount
    /// </summary>
    public int enemyWaveIncrease = 0;


    /*[Header("Preferences")]
    public bool useTriggers = false;
    public bool spawnEqually = false;*/

    private int spawnLocationNum = 0;
    private bool getBarriersActive;

    

    /// <summary>
    /// Checks whether or not a spawner is ready to spawn, then activates said spawner. 
    /// </summary>
    /// <param name="sp"> The spawner to check </param>
    /// <returns></returns>
    private bool checkSpawnValidity(Spawn sp)
    {
        return sp.CanSpawn(); 
    }
   public void setBarriersActive(bool active)
   {
        foreach(GameObject barrier in levelBarriers)
        {
            barrier.SetActive(active); 
        }
   }
    private void Update()
    {
        if (checkSpawnValidity(spawnLocations[spawnLocationNum]))
        {
            spawnLocations[spawnLocationNum].StartSpawnRoutine();
            Debug.Log($"spawner no {spawnLocationNum} activated!");
            spawnLocationNum++;  
        }
        else spawnLocationNum++;
        if (spawnLocationNum >= spawnLocations.Length) spawnLocationNum = 0; 
    }





}
