using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class appearAndDisappear : MonoBehaviour
{
    public Animator ani;

    void OnMouseOver()
    {
        ani.SetBool("runAnim", true);
    }

    void OnMouseExit()
    {
        ani.SetBool("runAnim", false);
    }
}
