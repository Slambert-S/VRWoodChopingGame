using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerWoodCutting : MonoBehaviour
{
    
    public bool gameIsStarted = false;
    public bool gameCanStart = true;

    public void Start()
    {
       

    }

    private void OnEnable()
    {
        if (GameEvents.current == null)
        {
            Debug.LogWarning("GameEvent is not yet created");
        }
        else
        {
            GameEvents.current.onGameStart += SetGameToActive;
            GameEvents.current.onGameOver += GameOver;
            GameEvents.onGameReset += GameReset;
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
            GameEvents.current.onGameStart -= SetGameToActive;
            GameEvents.current.onGameOver -= GameOver;
            GameEvents.onGameReset -= GameReset;
        }
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
