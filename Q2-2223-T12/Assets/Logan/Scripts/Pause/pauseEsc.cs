using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class pauseEsc : MonoBehaviour
{

    bool isPaused = false;
    public GameObject pauseMenu;

    public Animator lilypadAnim;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu = GameObject.Find("pauseMenu");
        pauseMenu.SetActive(false);
        lilypadAnim.SetBool("canPlay", true);
    }

    // Update is called once per frame
    void Update()
    {
        if(isPaused == false && Input.GetKeyDown(KeyCode.Escape))
        {
            
            pauseMenu.SetActive(true);
            //Time.timeScale = 0;
            lilypadAnim.SetTrigger("play");
            isPaused = true;
            Time.timeScale = 0;

        }
        if(isPaused == true && Input.GetKeyDown(KeyCode.Escape))
        {
            lilypadAnim.SetTrigger("playPt2");
            isPaused = false;
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
        }
    }
}
