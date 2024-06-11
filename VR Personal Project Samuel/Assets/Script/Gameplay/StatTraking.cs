using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatTraking : MonoBehaviour
{

    
    public static StatTraking current { get; private set; }

    private int goodLogHitCount;
    private int badLogHitCount;
    private int livesRemaining;
    
    [SerializeField]
    private GameManagerWoodCutting gameManagerRef;

    [SerializeField]
    private AudioClip _goodHit;

    [SerializeField]
    private AudioClip _badHit;

    [SerializeField]
    private int nbStartingLife = 3;      
    private void Awake()
    {
        //nbStartingLife = 3;

        if (current != null && current != this)
        {
            Destroy(this);
        }
        else
        {
            current = this;
            goodLogHitCount = 0;
            badLogHitCount = 0;
            livesRemaining = nbStartingLife;
        }
       
    }

    private void Start()
    {
        GameEvents.onGameReset += ResetStat;
    }

    public void RemoveLife()
    {
        livesRemaining -= 1;
       // GameUIManager.current.UpdateLive(livesRemaining);
        if(livesRemaining == 0)
        {
            GameEvents.current.GameOver();
        }
    }
    public int GetLifeRemaining()
    {
        return livesRemaining;
    }

    public void IncreasGoodLogHit()
    {
        goodLogHitCount++;
        gameManagerRef.UpdateGameState(GetTotalLogHit());
        PlaySoundController.current.PlayOneShot(_goodHit);


        //GameUIManager.current.UpdateGoodHit(goodLogHitCount);
    }
  
    public int GetGoodLogHitCount()
    {
        return goodLogHitCount;
    }

    public void IncreasBadLogHit()
    {
        badLogHitCount++;
        gameManagerRef.UpdateGameState(GetTotalLogHit());
        PlaySoundController.current.PlayOneShot(_badHit);
        //  GameUIManager.current.UpdateBadHit(badLogHitCount);
    }

    public int GetBadLogHitCount()
    {
        return badLogHitCount;
    }

    public int GetTotalLogHit()
    {
        return goodLogHitCount + badLogHitCount;
    }


    private void ResetStat()
    {
        goodLogHitCount = 0;
        badLogHitCount = 0;
        livesRemaining = nbStartingLife;

       // GameUIManager.current.UpdateBadHit(badLogHitCount);
       // GameUIManager.current.UpdateGoodHit(goodLogHitCount);
       // GameUIManager.current.UpdateLive(livesRemaining);
    }

    public int getNumberOfLifeLost()
    {
        int lifeLost = nbStartingLife - livesRemaining;
        return lifeLost;
    }
}
