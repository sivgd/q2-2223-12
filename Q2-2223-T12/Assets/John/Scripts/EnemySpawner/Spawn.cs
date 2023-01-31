using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [Header("Outside References")]
    public GameObject[] enemyQueue;
    public SpawnTrigger designatedTrigger;
    public SpawnController spawnController; 

    [Header("Preferences")]
    public int spawnsPerEnemy = 1;
    public bool increaseEnemiesPerWave = false; 
    public float spawnDelay = 0.2f;

    private int currentEnemy = 0;
    private int persistantIndex = 0; 
    private bool canSpawn = false;
    private GameObject[] instObj;
    private bool enemyExsists;


    private void Start()
    {
        instObj = new GameObject[enemyQueue.Length * spawnsPerEnemy];
    }    
    /// <summary>
    /// Spawns a new enemy if there aren't enemies in the scene. 
    /// <para>Iterates on what enemy can be spawned after it spawns an enemy and said enemy is destroyed</para>
    /// </summary>
    /// <returns></returns>
    private IEnumerator SpawnEnemy()
     {
        yield return new WaitUntil(() => canSpawn);
        StartCoroutine(EnemySpawn());
        spawnController.setBarriersActive(true); 
        yield return new WaitForSecondsRealtime(spawnDelay); 
        yield return new WaitWhile(() => enemyExsists);
        currentEnemy++;
        Debug.Log("Incrementing Current Enemy"); 
     }
    /// <summary>
    /// Spawns an enemy and adds them to the array of enemies exclusive to this spawnpoint 
    /// </summary>
    IEnumerator EnemySpawn()
    {
        Debug.Log($"Spawning {enemyQueue[currentEnemy].name}");
        for(int i = 0; i < spawnsPerEnemy; i++)
        {          
            instObj.SetValue(Instantiate(enemyQueue[currentEnemy], transform.position, transform.rotation), persistantIndex);
            instObj[persistantIndex].name += name;
            persistantIndex++; 
            yield return StartCoroutine(spawnTimer()); 
        }
        
        if (increaseEnemiesPerWave) spawnsPerEnemy += spawnController.enemyWaveIncrease;
       // StopCoroutine(EnemySpawn()); 
    }
    IEnumerator spawnTimer()
    {
        yield return new WaitForSecondsRealtime(spawnDelay); 
    }
    /// <summary>
    /// Checks if there are currently any enemies exclusive to this gameobject in the scene
    /// </summary>
    /// <returns></returns>
    private bool CheckForEnemy()
    {
        for(int enemies = 0; enemies < instObj.Length; enemies++)
        {
            if (instObj[enemies] != null) return true;
        }
        return false; 
    }
    public void StartSpawnRoutine()
    {
        StartCoroutine(SpawnEnemy());
        return; 
    }
    public bool CanSpawn()
    {
        return canSpawn; 
    }
    private void Update()
    {
        if (!enemyExsists) canSpawn = designatedTrigger.getActivated();
        else canSpawn = false;
        if (currentEnemy >= enemyQueue.Length)
        {
            canSpawn = false;
            spawnController.setBarriersActive(false); 
            gameObject.SetActive(false);
        }
        enemyExsists = CheckForEnemy(); 
       
    }
}
