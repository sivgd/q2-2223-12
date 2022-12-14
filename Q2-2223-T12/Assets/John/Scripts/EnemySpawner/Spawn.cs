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
    //public float spawnDelay = 0.2f;

    private int currentEnemy = 0;
    private bool canSpawn = false;
    private GameObject[] instObj;
    private bool enemyExsists;


    private void Start()
    {
        instObj = new GameObject[enemyQueue.Length]; 
    }
    /// <summary>
    /// Spawns a new enemy if there aren't enemies in the scene
    /// iterates on what enemy can be spawned after it spawns an enemy, and said enemy is destroyed
    /// <returns></returns>
    private IEnumerator SpawnEnemy()
     {
        yield return new WaitUntil(() => canSpawn);
        EnemySpawn();
        yield return new WaitWhile(() => enemyExsists);
        currentEnemy++;
     }
    /// <summary>
    /// Spawns an enemy and adds them to the array of enemies exclusive to this spawnpoint 
    /// </summary>
    void EnemySpawn()
    {
        Debug.Log($"Spawning {enemyQueue[currentEnemy].name}");
       for(int i = 0; i< spawnsPerEnemy; i++)
        {
            instObj.SetValue(Instantiate(enemyQueue[currentEnemy], transform.position, transform.rotation),currentEnemy);
            instObj[currentEnemy].name += name;
        }
    }
   
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
    /// <summary>
    /// Checks if there are currently any enemies exclusive to this gameobject in the scene
    /// </summary>
    /// <returns></returns>
    public bool CanSpawn()
    {
        return canSpawn; 
    }
    private void Update()
    {
        if (!enemyExsists) canSpawn = designatedTrigger.getActivated();
        else canSpawn = false;
        if (currentEnemy >= enemyQueue.Length) canSpawn = false;
        enemyExsists = CheckForEnemy(); 
    }
}
