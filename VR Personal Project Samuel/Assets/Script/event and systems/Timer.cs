using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isTimerOn = false;
    public int intervalTime = 5;
    public float targetTime = 0f;
    void Start()
    {
        targetTime = intervalTime;
       
    }

    private void OnEnable()
    {
        if (GameEvents.current == null)
        {
            Debug.LogWarning("GameEvent is not yet created");
        }
        else
        {
            GameEvents.current.onLogIsRemoved += ResetTimer;
            GameEvents.current.onGameStart += StartTimer;
            GameEvents.current.onGameOver += StopTimer;
            GameEvents.current.onStopTimer += StopTimer;
            GameEvents.onGameReset += ResetTimer;

        }
    }

    private void OnDisable()
    {
        if (GameEvents.current == null)
        {
            Debug.LogWarning("GameEvent is not yet created");
        }
        else
        {
            GameEvents.current.onLogIsRemoved -= ResetTimer;
            GameEvents.current.onGameStart -= StartTimer;
            GameEvents.current.onGameOver -= StopTimer;
            GameEvents.current.onStopTimer -= StopTimer;
            GameEvents.onGameReset -= ResetTimer;
        }
    }

    void Update()
    {
        if (isTimerOn)
        {
            targetTime -= Time.deltaTime;
            //float result = (Mathf.Round(targetTime * 10));
            int result = (Mathf.CeilToInt(targetTime ));
           // GameUIManager.current.UpdateCountdownTimer(result);
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
        ///GameUIManager.current.UpdateCountdownTimer(intervalTime);
        //isTimerOn = true;
    }

    public void GameOver()
    {
        StopTimer();
        
    }
    // how to stop / start / reset Timer

}
