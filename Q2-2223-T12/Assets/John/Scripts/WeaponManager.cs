using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class WeaponManager : MonoBehaviour
{
    [Header("External References")]
    public TextMeshProUGUI timerText; 
    [Header("Keybinds")]
    public KeyCode FirstSlotKeybind = KeyCode.Alpha1;
    public KeyCode SecondSlotKeybind = KeyCode.Alpha2;
    public KeyCode ThirdSlotKeybind = KeyCode.Alpha3;
    [Header("Weapons")]
    public ShotgunArm shotgunArm;
    //public GameObject slimeballArm; 
    public GameObject slimeballLauncher;
    public LilypadSniper lillypadLauncher; 

    [SerializeField] private bool w1Active = false, w2Active= false, w3Active = false;
    public bool w1Equipped = true, w2Equipped = true, w3Equipped = true;
    private SlimballLauncher sBL;
    private void Start()
    {
       sBL =  slimeballLauncher.GetComponent<SlimballLauncher>();
    }
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
        //timerText.text = $"Cooldwn: {sBL.getCoolDownTimer()}";
    }
   
    private void RefreshWeapons()
    {
        w1Active = w1Active && w1Equipped;
        w2Active = w2Active && w2Equipped;
        w3Active = w3Active && w3Equipped;
        SlimeballCoolDown();
        shotgunArm.enabled = w1Active; 
        slimeballLauncher.SetActive(w2Active);
        //slimeballArm.SetActive(w2Active); 
        lillypadLauncher.enabled = w3Active;
        Debug.Log($"WeaponManager: Weapon States: (Shotgun, {w1Active}) (Slimeball, {w2Active}) (Lillypad, {w3Active})");
    }
    private void SlimeballCoolDown()
    {
        
        if (sBL.getCoolDownTimer() <= 0f) return;
        else StartCoroutine(SlimeballTimer());  
        
    }
    IEnumerator SlimeballTimer()
    {
        yield return new WaitForEndOfFrame();
        sBL.setCoolDownTimer(sBL.getCoolDownTimer() - Time.deltaTime); 
    }
}

