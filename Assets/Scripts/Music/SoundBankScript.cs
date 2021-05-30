using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBankScript : MonoBehaviour
{

    public void Start()
    {
        PlayMainMenu();
    }
    public void SetVolume(float volume)
    {
        AkSoundEngine.SetRTPCValue("MainVolume", volume);
    }

    public float GetVolume()
    {
        float volume;
        int type = 1;
        AkSoundEngine.GetRTPCValue("MainVolume", 0, 0, out volume, ref type);
        return volume;
    }

    public void PlayMainMenu()
    {
        AkSoundEngine.PostEvent("Play_Main_Menu", gameObject);
    }
    public void StopMainMenu()
    {
        AkSoundEngine.PostEvent("Stop_Main_Menu", gameObject);
    }
    public void PlaySoundFire()
    {
        AkSoundEngine.PostEvent("Play_Tank_Shoot", gameObject);
    }
    public void PlaySoundMoving()
    {
        AkSoundEngine.PostEvent("Play_Tank_Moving", gameObject);
    }
    public void PlaySoundRotation()
    {
        AkSoundEngine.PostEvent("Play_Tank_Rotation", gameObject);
    }
}
