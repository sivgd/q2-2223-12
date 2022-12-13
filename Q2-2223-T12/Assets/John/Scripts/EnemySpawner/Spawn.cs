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
    private bool canSpawn,hasSpawnedEnemies = false; 
    /// <summary>
    /// Spawns a new enemy after a fixed time delay
    /// if the number of spawns is greater than the alloted spawns per enemy then it resets number of spawns and changes the current enemy to the next one in the queue 
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
               
            }
            else
            {
                spawns = 0;
                currentEnemy++; 
                StopCoroutine(SpawnEnemy());
            }
        }
        else
        {
            canSpawn = false;
            hasSpawnedEnemies = true; 
            StopCoroutine(SpawnEnemy());
        }
     }
   
    public void StartSpawnRoutine()
    {
       
        StartCoroutine(SpawnEnemy());
    }
    public bool CanSpawn()
    {
        return canSpawn; 
    }
    private void Update()
    {
        if (!hasSpawnedEnemies)
        {
            canSpawn = designatedTrigger.getActivated();
        }
    }
}
