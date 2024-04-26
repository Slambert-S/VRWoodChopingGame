using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatTraking : MonoBehaviour
{

    
    public static StatTraking current { get; private set; }

    private int goodLogHitCount;
    private int badLogHitCount;
    private int livesRemaining;

    private int nbStartingLife = 3;
    private void Awake()
    {
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
        GameEvents.current.onGameReset += ResetStat;
    }

    public void RemoveLife()
    {
        livesRemaining -= 1;
        GameUIManager.current.UpdateLive(livesRemaining);
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
        GameUIManager.current.UpdateGoodHit(goodLogHitCount);
    }

    public int GetGoodLogHitCount()
    {
        return goodLogHitCount;
    }

    public void IncreasBadLogHit()
    {
        badLogHitCount++;
        GameUIManager.current.UpdateBadHit(badLogHitCount);
    }

    public int GetBadLogHitCount()
    {
        return badLogHitCount;
    }

    private void ResetStat()
    {
        goodLogHitCount = 0;
        badLogHitCount = 0;
        livesRemaining = nbStartingLife;
        GameUIManager.current.UpdateBadHit(badLogHitCount);
        GameUIManager.current.UpdateGoodHit(goodLogHitCount);
        GameUIManager.current.UpdateLive(livesRemaining);
    }
}
