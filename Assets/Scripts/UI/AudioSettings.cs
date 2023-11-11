using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSettings : MonoBehaviour
{
    public static AudioSettings instance;

    float SFXVolume;   // 0.0f to 1.0f
    float MusicVolume; // 0.0f to 1.0f
    bool isMuted;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        LoadAudioPrefs();
        SetAudiosInScene();
    }    

    public int GetMusicVolume()
    {
        Debug.Log("Music: " + ((int)(MusicVolume * 100)));
        return (int)(MusicVolume * 100);// 0 to 100
    }
    public int GetSXFVolume()
    {
        Debug.Log("SFX: " + ((int)(SFXVolume * 100)));
        return (int)(SFXVolume * 100);// 0 to 100
    }
    public float GetNormalizedMusicVolume()
    {
        return MusicVolume;// 0.0f to 1.0f
    }
    public float GetNormalizedSXFVolume()
    {
        return SFXVolume;// 0.0f to 1.0f
    }
    public bool GetIsMuted()
    {
        return isMuted;
    }
    public void SetMusicVolume(float sliderValue)
    {
        MusicVolume = sliderValue;
    }
    public void SetSXFVolume(float sliderValue)
    {
        SFXVolume = sliderValue;
    }
    public void SetIsMuted(bool muted)
    {
        isMuted = muted;
    }

    public void LoadAudioPrefs()
    {
        SFXVolume = ((float)PlayerSettings.instance.GetSXFVolume() / 100.0f );
        MusicVolume = ((float)PlayerSettings.instance.GetMusicVolume() / 100.0f);
        isMuted = PlayerSettings.instance.GetIsMuted();
    }
    public void SetAudiosInScene()
    {
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
