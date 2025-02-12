using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;
    [SerializeField] private float defaultTime = 10f;
    [SerializeField] private Animator myAnimator;
    public UnityEvent threeSeconds;

    // Update is called once per frame
    void Update()
    {
        if(remainingTime > 0) 
        {
            remainingTime -= Time.deltaTime;
        }
        else if(remainingTime < 0)
        {
            remainingTime = 0;
        }

        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if(seconds == 3) {
            //myAnimator.setTrigger("countdown");
        }
    }

    public void ResetTimer()
    {
        remainingTime = defaultTime;
    }

    
}
