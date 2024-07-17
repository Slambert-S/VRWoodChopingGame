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
        GameEvents.current.onGameReset += hideButtonWorkAround;
    }


    public void RestartGame()
    {
        //DisplayRestartButton(false);

        GameEvents.current.GameReset();
        //DisplayRestartButton(false);
    }
    private void GameOver()
    {
        DisplayRestartButton(true);
        // Display the information you want. 
        // Hide life show more stat

    }
    public void hideButtonWorkAround()
    {
        DisplayRestartButton(false);
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

    public void UpdateFinalTime(int time)
    {
        TimerUIRef.text = "Final time : <br> " + time + " Second";
    }

}
