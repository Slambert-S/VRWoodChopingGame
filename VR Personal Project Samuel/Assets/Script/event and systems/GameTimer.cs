using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    public int totalTime = 0;
    // Start is called before the first frame update
   

    private void OnEnable()
    {
        if (GameEvents.current == null)
        {
            Debug.LogWarning("GameEvent is not yet created");
        }
        else
        {
            GameEvents.current.onGameStart += StartTimer;
            GameEvents.current.onGameOver += StopTimer;
            GameEvents.current.onGameReset += ResetTimer;

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

            GameEvents.current.onGameStart -= StartTimer;
            GameEvents.current.onGameOver -= StopTimer;
            GameEvents.current.onGameReset -= ResetTimer;
        }
    }
    private void StartTimer()
    {
        StartCoroutine(TimerRoutine());
    }

    private void StopTimer()
    {
        StopCoroutine(TimerRoutine());
        //Display score on the UI
        GameUIManager.current.UpdateFinalTime(totalTime);
    }

    private void ResetTimer()
    {
        Debug.LogError("In reset time Before :" + totalTime);
        totalTime = 0;
        Debug.LogWarning(" in reset time");
        Debug.LogError("In reset time After :" + totalTime);
    }

    IEnumerator TimerRoutine()
    {
        totalTime = 0;
        WaitForSeconds delay = new WaitForSeconds(1);
        while (true)
        {
            totalTime += 1;
            yield return delay;
        }
    }
}
