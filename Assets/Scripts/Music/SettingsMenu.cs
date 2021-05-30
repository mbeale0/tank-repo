using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
[System.Serializable]
    public class SettingsMenu : MonoBehaviour
    {
        private SoundBankScript _SoundBank;
        Slider _Volume;
        void Start()
        {
            _SoundBank = GameObject.Find("SoundBank").GetComponent<SoundBankScript>();
            _Volume = GameObject.Find("Volume").GetComponent<Slider>();
            _Volume.value = _SoundBank.GetVolume();
        }

        public void SetVolume()
        {
            _SoundBank.SetVolume(_Volume.value);
        }
    }

