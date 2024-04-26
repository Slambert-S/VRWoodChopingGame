using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerWoodCutting : MonoBehaviour
{
    
    public bool gameIsStarted = false;
    public bool gameCanStart = true;

    public void Start()
    {
        GameEvents.current.onGameStart += SetGameToActive;
        GameEvents.current.onGameOver += GameOver;
        GameEvents.current.onGameReset += GameReset;

    }

    public void SetGameToPaused()
    {
        gameIsStarted = false;
    }

    public void SetGameToActive()
    {
        gameIsStarted = true;
    }

    public void SetGameCanStart(bool value)
    {
        gameCanStart = value;
    }

    private void GameOver()
    {
        SetGameToPaused();
        SetGameCanStart(false);
    }

    private void GameReset()
    {
        SetGameCanStart(true);
    }

    

}
