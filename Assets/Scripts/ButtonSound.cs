using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    public void onClick()
    {
        AkSoundEngine.PostEvent("Play_Click", gameObject);
    }
}
