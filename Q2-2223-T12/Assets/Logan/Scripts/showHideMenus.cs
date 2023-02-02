using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showHideMenus : MonoBehaviour
{
    public GameObject menuToHide;
    public GameObject menuToShow;


    public void onClickThing()
    {
        if (menuToHide != null) menuToHide.SetActive(false);
        if (menuToShow != null) menuToShow.SetActive(true);
    }

}
