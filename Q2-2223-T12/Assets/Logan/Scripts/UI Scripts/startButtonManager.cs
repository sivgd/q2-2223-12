using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startButtonManager : MonoBehaviour
{

    public string startButtonScene;
    private GameObject creditSceneCanvas;

    private void Start()
    {
        creditSceneCanvas = GameObject.Find("creditSceneCanvas"); 
    }

    public void startButtonClicked()
    {
        SceneManager.LoadScene(startButtonScene);
    }

    public void creditsScene()
    {
        
    }

}
