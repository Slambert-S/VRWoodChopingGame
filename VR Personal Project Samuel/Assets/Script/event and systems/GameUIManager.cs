using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUIManager : MonoBehaviour
{
    public TMP_Text lifeUIRef;
    public TMP_Text goodHitUIRef;
    public TMP_Text BadHitUIRef;
    public TMP_Text TimerUIRef;
    // Start is called before the first frame update

    public static GameUIManager current;
    private void Awake()
    {
        current = this;
    }

    public void UpdateCountdownTimer(float time)
    {
        string text = "Time : " + time;
        TimerUIRef.text = text;
    }

    public void UpdateLive(int value)
    {
        lifeUIRef.text = "Life : " + value; 
    }

    public void UpdateGoodHit(int value)
    {
        goodHitUIRef.text = "Good hit : " + value;
    }

    public void UpdateBadHit(int value)
    {
        BadHitUIRef.text = "Bad hit : " + value;
    }

}
