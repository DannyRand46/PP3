using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSliders : MonoBehaviour
{
    [SerializeField] Slider MusicSlider;
    [SerializeField] Slider SFXSlider;
    [SerializeField] Toggle MuteCheck;
    // Start is called before the first frame update
    void Start()
    {
        SetSliders();
    }

    public void SetSliders()
    {
        MusicSlider.value = AudioSettings.instance.GetNormalizedMusicVolume();
        SFXSlider.value = AudioSettings.instance.GetNormalizedSXFVolume();
        MuteCheck.isOn = AudioSettings.instance.GetIsMuted();
    }
    public void ResetToDefault()
    {
        MusicSlider.value = 0.5f;
        SFXSlider.value = 0.5f;
        MuteCheck.isOn = false;
    }
}
