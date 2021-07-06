using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
[System.Serializable]
    public class SettingsMenu : MonoBehaviour
    {
    public Slider thisSlider;
    public float masterVolume;


    public void SetSpecificVolume(string whatValue)
    {
        float sliderValue = thisSlider.value;
        
        if (whatValue == "Master")
        {
            masterVolume = thisSlider.value;
            //AkSoundEngine.SetRTPCValue("MasterVolume", masterVolume);
        }
    }
    }

