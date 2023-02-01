using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class panelMove : MonoBehaviour
{
    public GameObject[] comicPanels;
    public int currentPanel;
    public int lastPanel;

    private void Update()
    {
        for (int i = 0; i < comicPanels.Length; i++)
        {
            lastPanel = i;
        }

    }

    public void mouseClick()
    {
        if(comicPanels[currentPanel] != comicPanels[10])
        {
            Debug.Log("a"); ;
            comicPanels[currentPanel].SetActive(false);
            currentPanel++;
        }
        else if(comicPanels[currentPanel] == comicPanels[10])
        {
            SceneManager.LoadScene("Level", LoadSceneMode.Single);
        }
    }

}
