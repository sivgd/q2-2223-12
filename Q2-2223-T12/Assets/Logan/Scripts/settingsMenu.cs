using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class settingsMenu : MonoBehaviour
{

    private void Start()
    {
        Screen.fullScreen = true;
    }

    public AudioMixer masterVolumeMixer;
    public AudioMixerGroup sfxVolumeMixer;
    public AudioMixerGroup musicVolumeMixer;

    public void setMasterVolume(float mVolume)
    {
        masterVolumeMixer.SetFloat("masterVolume", mVolume);
    }

    public void setSFXVolume(float sVolume)
    {
        masterVolumeMixer.SetFloat("sfxVolume", sVolume);
    }

    public void setMusicVolume(float muVolume)
    {
        masterVolumeMixer.SetFloat("musicVolume", muVolume);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
