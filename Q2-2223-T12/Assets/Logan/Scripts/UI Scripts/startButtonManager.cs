using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startButtonManager : MonoBehaviour
{

    public string startButtonScene;
    private GameObject creditSceneCanvas;
    private GameObject mainMenuCanvas;
    public GameObject creditSceneMoveThing;
    public float yPositionStopThing;

    private void Start()
    {

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

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
        creditSceneMoveThing.transform.localPosition =  new Vector3(0, -4523, 0);
        creditSceneCanvas.SetActive(true);
        mainMenuCanvas.SetActive(false);
    }

    public void unCreditsScene()
    {
        creditSceneCanvas.SetActive(false);
        mainMenuCanvas.SetActive(true);
    }

    public void exitGame()
    {
        Application.Quit();
    }

    private void Update()
    {
        if(creditSceneMoveThing.transform.localPosition.y >= yPositionStopThing)
        {
            unCreditsScene();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            unCreditsScene();
        }
    }

    

}
