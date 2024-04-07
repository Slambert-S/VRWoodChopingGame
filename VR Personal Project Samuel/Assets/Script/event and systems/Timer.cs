using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isTimerOn = false;
    public float intervalTime = 5.0f;
    public float targetTime = 0f;
    void Start()
    {
        targetTime = intervalTime;
        GameEvents.current.onLogIsRemoved += ResetTimer;
        GameEvents.current.onGameStart += StartTimer;
    }

    void Update()
    {
        if (isTimerOn)
        {
            targetTime -= Time.deltaTime;
            float result = (Mathf.Round(targetTime * 10)/10);
            GameUIManager.current.UpdateCountdownTimer(result);
            if (targetTime <= 0.0f)
            {
                timerEnded();
            }
        }

    }

    void timerEnded()
    {
        //do your stuff here.
        GameEvents.current.TimerIsOver();
        ResetTimer();
    }

    public void StartTimer()
    {
        isTimerOn = true;
    }

    public void StopTimer()
    {
        isTimerOn = false;
    }

    public void ResetTimer()
    {
        // isTimerOn = false;
        targetTime = intervalTime;
        //isTimerOn = true;
    }
    // how to stop / start / reset Timer

}
