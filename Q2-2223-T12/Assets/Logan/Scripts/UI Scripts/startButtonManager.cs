using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startButtonManager : MonoBehaviour
{

    public string startButtonScene;
    private GameObject creditSceneCanvas;
    private GameObject mainMenuCanvas;

    private void Start()
    {
        creditSceneCanvas = GameObject.Find("creditSceneCanvas"); 
        mainMenuCanvas = GameObject.Find("mainMenuCanvas");

        creditSceneCanvas.SetActive(false);

    }

    public void startButtonClicked()
    {
        SceneManager.LoadScene(startButtonScene);
    }

    public void creditsScene()
    {
        creditSceneCanvas.SetActive(true);
        mainMenuCanvas.SetActive(false);
    }

    public void unCreditsScene()
    {
        creditSceneCanvas.SetActive(false);
        mainMenuCanvas.SetActive(true);
    }

}
