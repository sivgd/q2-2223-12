using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class DeathUIButtons : MonoBehaviour
{
    
   public static void ResetScene()
   {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
   }
   public static void LoadMenu()
   {
        SceneManager.LoadScene(0); 
   }
}
