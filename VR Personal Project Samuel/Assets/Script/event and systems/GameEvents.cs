using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    private void Awake()
    {
        current = this;
    }

    public event Action onTimerOver;
    public void TimerIsOver()
    {
        if (onTimerOver != null)
        {
            onTimerOver();
        }
    }

    public event Action onLogIsRemoved;
    public void LogIsRemoved()
    {
        if(onLogIsRemoved != null)
        {
            onLogIsRemoved();
        }
    }

    public event Action onGameStart;
    public void GameIsStarted()
    {
        if(onGameStart != null)
        {
            onGameStart();
        }
    }

    public event Action onGameStop;
    public void GameIsStoped()
    {
        if(onGameStop != null)
        {
            onGameStop();
        }
    }

    public event Action onGameOver;
    public void GameOver()
    {
        if (onGameOver != null)
        {
            onGameOver();
        }
    }

    public event Action onGameReset;
    public void GameReset()
    {
        if (onGameReset != null)
        {
            onGameReset();
        }
    }



}
