using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManagerWoodCutting : MonoBehaviour
{
    
    public bool gameIsStarted = false;
    public bool gameCanStart = true;
    public bool canCrateNewLog = true;

    private gameLevel _gameLevel = gameLevel.start;
    [SerializeField]
    private List<int> levelTreshold = new List<int>();

    [SerializeField]
    private  const int nbLoogToChopToWIngGame = 41;
    int numberofHitBeforeStopingGeneratingLog = 0;
    public TMP_Text debugLevelDisplay;
    public TMP_Text debugGoodLogHitDisplay;


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
            GameEvents.current.onGameReset += GameReset;
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
            GameEvents.current.onGameReset -= GameReset;
        }
    }

    public gameLevel GetGameLevel()
    {
        return _gameLevel;
    }


    public void UpdateGameState(int totalLogHit)
    {
        debugGoodLogHitDisplay.text = "Hit number : " + StatTraking.current.GetGoodLogHitCount();

        if (StatTraking.current.GetGoodLogHitCount() == nbLoogToChopToWIngGame - 3 && numberofHitBeforeStopingGeneratingLog==0)
        {
            numberofHitBeforeStopingGeneratingLog = (totalLogHit + StatTraking.current.GetLifeRemaining());
        }
        
        //Check if the level need to increase.
        //(int)_gameLevel) - 1 because we don't take into acount the first hit/level (start) 
        int levelValue = ((int)_gameLevel) - 1;
        VRDebugConsol.Instance.LogMessageToConsol("Level value :" + levelValue);
        VRDebugConsol.Instance.LogMessageToConsol("Level capacity :" + levelTreshold.Capacity);
        if (levelValue != -1)
        {
            if(levelValue < levelTreshold.Capacity)
            {

                try
                {
                    if (StatTraking.current.GetGoodLogHitCount() == levelTreshold[levelValue])
                    {
                        IncreaseLevel();
                    }
                }
                catch (System.Exception)
                {

                    throw;
                }
            
            }
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
        _gameLevel = gameLevel.start;
    }

    public void IncreaseLevel()
    {
        switch (_gameLevel)
        {
            case gameLevel.start:
                _gameLevel = gameLevel.one;
                break;
            case gameLevel.one:
                _gameLevel = gameLevel.two;
                break;
            case gameLevel.two:
                _gameLevel = gameLevel.tree;
                break;
            case gameLevel.tree:
                _gameLevel = gameLevel.four;
                break;
            case gameLevel.four:
                break;
            default:
                break;
        }
        //Display the debug levelà
        debugLevelDisplay.text = " Current Level : " + _gameLevel;

    }
    public enum gameLevel
    {
        start,
        one,
        two,
        tree,
        four

    }

}
