using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerWoodCutting : MonoBehaviour
{
    
    public bool gameIsStarted = false;
    public bool gameCanStart = true;
    public bool canCrateNewLog = true;
    
    [SerializeField]
    private  const int nbLoogToChopToWIngGame = 15;
    int numberofHitBeforeStopingGeneratingLog = 0;

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


    public void UpdateGameState(int totalLogHit)
    {
        VRDebugConsol.Instance.LogMessageToConsol("Reached UpdateGameState");
        
        /*
        switch (nbCutLog)
        {
            case 5:
                // do nothing for now;

                break;
            case numberofHitBeforeStopingGeneratingLog:
                // stop generating new log;
                canCrateNewLog = false;
                
                break;
            case nbLoogToChopToWIngGame:
                //end game;
                GameOver();
                GameEvents.current.GameOver();

                break;
            default:
                break;
        }
        */

        if(StatTraking.current.GetGoodLogHitCount() == nbLoogToChopToWIngGame - 3 && numberofHitBeforeStopingGeneratingLog==0)
        {
            numberofHitBeforeStopingGeneratingLog = (totalLogHit + StatTraking.current.GetLifeRemaining());
        }

        if(numberofHitBeforeStopingGeneratingLog != 0 && totalLogHit == numberofHitBeforeStopingGeneratingLog)
        {
            // stop generating new log;
            canCrateNewLog = false;
        }

        if(StatTraking.current.GetGoodLogHitCount() == nbLoogToChopToWIngGame)
        {
            //end game;
            GameOver();
            GameEvents.current.GameOver();

        }


        /*
         * good hit | bad hit | total hit 
         * 
         * 
         * stop generating log 12 + life remaining
         * 
         * 3 life remaining => 12 + 3 = 15 total hit
         * o 13
         * o 14
         * o 15
         * o
         * o
         * o
         * 
         * 
         * 
         * finit a 15 good hit
         * 
         */
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
        canCrateNewLog = true;
    }

    

}
