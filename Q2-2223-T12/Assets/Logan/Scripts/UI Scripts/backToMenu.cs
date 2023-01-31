using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class backToMenu : MonoBehaviour
{

    public int menuScene;

    public void onButtonClick()
    {
        SceneManager.LoadScene(menuScene);
    }
}
