using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public GameObject[] weapons = new GameObject[3];
    [SerializeField] private bool w1Active = false, w2Active= false, w3Active = false;

    public void Update()
    {
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
        ActiveWeapons(); 
    }
    private void ActiveWeapons()
    {
        weapons[0].SetActive(w1Active);
        weapons[1].SetActive(w2Active);
        weapons[2].SetActive(w3Active);
        Debug.Log($"weapon 1 active: {weapons[0].activeInHierarchy}\nweapon 2 active: {weapons[1].activeInHierarchy}\nweapon 3 active: {weapons[2].activeInHierarchy}"); 
    }
}
