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
    private void Start()
    {
        StartCoroutine(checkSpawnValidity()); 
    }
    /// <summary>
    /// Checks whether or not a spawner is ready to spawn, then activates said spawner. 
    /// </summary>
    /// <returns></returns>
    IEnumerator checkSpawnValidity()
    {
        yield return new WaitForEndOfFrame();
        for (int i = 0; i < spawnLocations.Length; i++)
        {
            if (spawnLocations[i].CanSpawn())
            {
                spawnLocations[i].StartSpawnRoutine(waveDelay);
                Debug.Log($"{spawnLocations[i].name} can spawn");
            }
        }

    }



}
