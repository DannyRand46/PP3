using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSettings : MonoBehaviour
{
    public static PlayerSettings instance;

    string SFXVolumeKey = "SFX";
    string MusicVolumeKey = "Music";
    string MutedKey = "Muted";

    void Awake()
    {
        instance = this;
    }

    public void SavePrefs()
    {
        PlayerPrefs.SetInt(MusicVolumeKey, AudioSettings.instance.GetMusicVolume());
        PlayerPrefs.SetInt(SFXVolumeKey, AudioSettings.instance.GetSXFVolume());
        PlayerPrefs.SetInt(MutedKey, AudioSettings.instance.GetIsMuted() ? 1 : 0 ) ;
        PlayerPrefs.Save();
    }

    public void ResetDefaults()
    {
        PlayerPrefs.DeleteAll();
    }

    public int GetMusicVolume()
    {
         return PlayerPrefs.GetInt(MusicVolumeKey, 50);
    }
    public int GetSXFVolume()
    {
        return PlayerPrefs.GetInt(SFXVolumeKey, 50);
    }
    public bool GetIsMuted()
    {
        return (PlayerPrefs.GetInt(MutedKey, 0) == 1);
    }
}
