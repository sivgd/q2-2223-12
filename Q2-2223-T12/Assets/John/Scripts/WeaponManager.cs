using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [Header("Keybinds")]
    public KeyCode FirstSlotKeybind = KeyCode.Alpha1;
    public KeyCode SecondSlotKeybind = KeyCode.Alpha2;
    public KeyCode ThirdSlotKeybind = KeyCode.Alpha3;
    [Header("Weapons")]
    public GameObject shotgunArm;
    public GameObject slimeballLauncher;
    public GameObject lillypadLauncher; 

    [SerializeField] private bool w1Active = false, w2Active= false, w3Active = false;
    public bool w1Equipped = true, w2Equipped = true, w3Equipped = true; 
    public void Update()
    {
        /*
         * 1 = Shotgun Fist
         * 2 = Slimeball launcher (big boom)
         * 3 = Lillypad Sniper 
         */
        if (Input.GetKey(FirstSlotKeybind))
        {
            w1Active = true;
            w2Active = false;
            w3Active = false; 
        }
        if (Input.GetKey(SecondSlotKeybind))
        {
            w1Active = false;
            w2Active = true;
            w3Active = false;
        }
        if (Input.GetKey(ThirdSlotKeybind))
        {
            w1Active = false;
            w2Active = false;
            w3Active = true;
        }
        RefreshWeapons(); 
    }
   
    private void RefreshWeapons()
    {
        w1Active = w1Active && w1Equipped;
        w2Active = w2Active && w2Equipped;
        w3Active = w3Active && w3Equipped; 

        shotgunArm.SetActive(w1Active); 
        slimeballLauncher.SetActive(w2Active);
        lillypadLauncher.SetActive(w3Active);
        if (!w2Active) slimeballLauncher.GetComponent<SlimballLauncher>().resetCharge(); 
        Debug.Log($"WeaponManager: Weapon States: (Shotgun, {w1Active}) (Slimeball, {w2Active}) (Lillypad, {w3Active})");
    }
}

