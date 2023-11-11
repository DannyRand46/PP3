using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSettings : MonoBehaviour
{
    public static AudioSettings instance;

    float SFXVolume;
    float MusicVolume;
    bool isMuted;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        LoadAudioPrefs();
    }    

    public int GetMusicVolume()
    {
        return (int)(MusicVolume * 100);
    }
    public int GetSXFVolume()
    {
        return (int)(SFXVolume * 100);
    }
    public bool GetIsMuted()
    {
        return isMuted;
    }

    public void LoadAudioPrefs()
    {
        SFXVolume = (PlayerSettings.instance.GetSXFVolume() / 100.0f );
        MusicVolume = (PlayerSettings.instance.GetMusicVolume() / 100.0f);
        isMuted = PlayerSettings.instance.GetIsMuted();

        // Set all audio sources in the scene to the volume from Player Prefs;
        AudioSource[] aSources = FindObjectsOfType<AudioSource>();
        Debug.Log("Found " + aSources.Length.ToString() + " Audio Sources");
        for (int i = 0; i < aSources.Length; i++)
        {
            aSources[i].mute = isMuted;

            if (aSources[i].gameObject.CompareTag("Music"))
            {
                aSources[i].volume = MusicVolume;
            }
            else if (aSources[i].gameObject.CompareTag("SFX"))
            {
                aSources[i].volume = SFXVolume;
            }
        }
    }
}
