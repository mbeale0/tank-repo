using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RechargeTimer : MonoBehaviour
{
    public Slider timerSlider;

   

    public float rechargeTime;

    private bool stopTimer;
    // Start is called before the first frame update
    void Start()
    {
        stopTimer = false;
        timerSlider.maxValue = rechargeTime;
        timerSlider.value = rechargeTime;
    }

    // Update is called once per frame
 
    public void rechargeTimer()
    {
        float time = rechargeTime - Time.time;

        int seconds = Mathf.FloorToInt(time / 60);


        if (time <= 0)
        {
            stopTimer = true;
        }

        if (stopTimer == false)
        {
            timerSlider.value = time;
        }
    }
}
