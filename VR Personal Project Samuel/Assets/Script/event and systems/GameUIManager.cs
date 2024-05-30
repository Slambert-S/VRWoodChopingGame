using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUIManager : MonoBehaviour
{
    public TMP_Text lifeUIRef;
    public TMP_Text goodHitUIRef;
    public TMP_Text BadHitUIRef;
    public TMP_Text TimerUIRef;
    public Button restartButtonRef;
    // Start is called before the first frame update

    public static GameUIManager current;
    private void Awake()
    {
        current = this;
        //UpdateLive(StatTraking.current.GetLifeRemaining());
    }

    private void Start()
    {
        GameEvents.current.onGameOver += GameOver;
    }

   /* 
    public void UpdateCountdownTimer(int time)
    {
        string text = "Time : " + time;
        TimerUIRef.text = text;
    }
   */
   
    /*
    public void UpdateLive(int value)
    {
        lifeUIRef.text = "Life : " + value; 
    }
   */

    /*
    public void UpdateGoodHit(int value)
    {
        goodHitUIRef.text = "Good hit : " + value;
    }
    */
    
    /*
    public void UpdateBadHit(int value)
    {
        BadHitUIRef.text = "Bad hit : " + value;
    }*/

    public void RestartGame()
    {
        //DisplayRestartButton(false);

        GameEvents.current.GameReset();
        DisplayRestartButton(false);
    }
    private void GameOver()
    {
        DisplayRestartButton(true);
        // Display the information you want. 
        // Hide life show more stat

    }

    public void DisplayRestartButton(bool value)
    {
        if(value == true)
        {
            restartButtonRef.gameObject.SetActive(true);
            this.GetComponent<Canvas>().enabled = true;
        }
        else
        {
            restartButtonRef.gameObject.SetActive(false);
            this.GetComponent<Canvas>().enabled = false;
        }
    }

}
