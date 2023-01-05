using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public GameObject[] weapons = new GameObject[3];
    [SerializeField] private bool w1Active = false, w2Active= false, w3Active = false;

    ///@TODO: impliment weapon pickup / progression

    public void Update()
    {
        /*
         * 1 = Shotgun Fist
         * 2 = Slimeball launcher (big boom)
         * 3 = Lillypad Sniper 
         */
        if (Input.GetKey(KeyCode.Alpha1))
        {
            w1Active = true;
            w2Active = false;
            w3Active = false; 
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            w1Active = false;
            w2Active = true;
            w3Active = false;
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            w1Active = false;
            w2Active = false;
            w3Active = true;
        }
        RefreshWeapons(); 
    }
    private void RefreshWeapons()
    {
        weapons[0].SetActive(w1Active);
        weapons[1].SetActive(w2Active);
        weapons[2].SetActive(w3Active);
        Debug.Log($"weapon 1 active: {weapons[0].activeInHierarchy}\nweapon 2 active: {weapons[1].activeInHierarchy}\nweapon 3 active: {weapons[2].activeInHierarchy}"); 
    }

}
