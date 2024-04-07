using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatTraking : MonoBehaviour
{

    
    public static StatTraking current { get; private set; }

    private int goodLogHitCount;
    private int badLogHitCount;
    private int livesRemaining;
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
            livesRemaining = 3;
        }
       
    }
   
    public void RemoveLife()
    {
        livesRemaining -= 1;
        GameUIManager.current.UpdateLive(livesRemaining);
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

}
