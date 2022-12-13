using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [Header("Outside References")]
    public GameObject[] enemyQueue;
    public SpawnTrigger designatedTrigger; 

    [Header("Preferences")]
    public int spawnsPerEnemy = 1;
    public float spawnDelay = 0.2f;
   
    private int currentEnemy = 0;
    private int spawns = 0;
    private int totalSpawns; 
    /// <summary>
    /// Spawns a new enemy after a fixed time delay
    /// if the number of spawns is greater than the alloted spawns per enemy then it resets number of spawns and spawns the next enemy 
    /// </summary>
    /// <returns></returns>
     private IEnumerator SpawnEnemy()
     {
        yield return new WaitForSecondsRealtime(spawnDelay);
        if (currentEnemy < enemyQueue.Length)
        {
            GameObject currentSpawn = enemyQueue[currentEnemy];
            if (spawns < spawnsPerEnemy)
            {
                Instantiate(currentSpawn, transform.position, transform.rotation);
                spawns++;
                totalSpawns++; 
            }
            else
            {
                spawns = 0;
                currentEnemy++; 
                StopCoroutine(SpawnEnemy());
            }
        }
        else StopCoroutine(SpawnEnemy());
     }
    /// <summary>
    /// Spawns an enemy after a given wave delay
    /// </summary>
    /// <param name="delay"></param> the delay between waves 
    /// <returns></returns>
    public IEnumerator StartSpawnRoutine(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        StartCoroutine(SpawnEnemy());
    }
    public bool CanSpawn()
    {
        int spawnsLeft = spawnsPerEnemy - (totalSpawns * enemyQueue.Length);
        bool output = designatedTrigger && (spawnsLeft <= 0);
        return output; 
    }
}
