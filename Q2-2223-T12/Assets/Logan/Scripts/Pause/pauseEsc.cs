using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class pauseEsc : MonoBehaviour
{

    private GameObject orientation;

    bool isPaused = false;
    public GameObject pauseMenu;

    public Animator lilypadAnim;

    bool canPause;
    public bool canUnpause;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu = GameObject.Find("pauseMenu");
        orientation = GameObject.Find("Main Camera");
        pauseMenu.SetActive(false);
        canPause = true;
        canUnpause = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isPaused == false && Input.GetKeyDown(KeyCode.Escape) && canPause == true)
        {

            canUnpause = false;
            StartCoroutine(allowUnpauseAction());

            canPause = false;
            pauseMenu.SetActive(true);
            //Time.timeScale = 0;
            lilypadAnim.SetTrigger("play");
            isPaused = true;
            Time.timeScale = 0;

            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            orientation.GetComponent<camMove>().enabled = false;
        }

        else if (Input.GetKeyDown(KeyCode.Escape) && canUnpause == true)
        {
            canPause = false;
            lilypadAnim.SetTrigger("playPt2");
            isPaused = false;
            Time.timeScale = 1;
            //pauseMenu.SetActive(false);
            Invoke("unpauseThing", 0.8f);
            disallowPause();

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            orientation.GetComponent<camMove>().enabled = true;
        }
    }

    public void resumeButton()
    {
        canPause = false;
        lilypadAnim.SetTrigger("playPt2");
        isPaused = false;
        Time.timeScale = 1;
        Invoke("unpauseThing", 0.8f);
        Invoke("allowPause", 2);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        orientation.GetComponent<camMove>().enabled = true;

    }

    private void unpauseThing()
    {
        pauseMenu.SetActive(false);
    }

    private void allowPause()
    {
        canPause = true;
    }

    private void disallowPause()
    {
        canPause = false;
        Invoke("allowPause", 2);
    }

    void allowUnpause()
    {
        canUnpause = true;
    }

    IEnumerator allowUnpauseAction()
    {
        yield return new WaitForSecondsRealtime(2);
        canUnpause = true;
    }    

}
