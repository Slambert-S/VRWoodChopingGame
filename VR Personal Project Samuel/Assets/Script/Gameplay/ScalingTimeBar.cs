using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScalingTimeBar : MonoBehaviour
{
    // Start is called before the first frame update
    public float animationTime;
    private Vector3 originalScale;
    public Color startcolor;
    public Color endColor;
    void Start()
    {
        
    }

    private void OnEnable()
    {
        originalScale = this.transform.localScale;
        HideBar();
        if (GameEvents.current == null)
        {
            Debug.LogWarning("GameEvent is not yet created");
        }
        else
        {
            GameEvents.current.onLogIsRemoved += ResetBar;
            GameEvents.current.onGameStart += GameStart;
            GameEvents.current.onGameOver += EndOfGame;
            //GameEvents.onGameReset += ResetTimer;
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
            GameEvents.current.onLogIsRemoved -= ResetBar;
            GameEvents.current.onGameStart -= GameStart;
            GameEvents.current.onGameOver -= EndOfGame;
            //GameEvents.onGameReset += ResetTimer;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void AnimateBar()
    {
        LeanTween.scaleX(this.gameObject, 0, animationTime);
        LeanTween.value(gameObject, setColorCallback, startcolor, endColor, 5f);
    }

    public void GameStart()
    {
        ShowBar();
        ResetBar();
    }
    public void ResetBar()
    {
        //ShowBar();
        LeanTween.cancel(this.gameObject);
        this.gameObject.transform.localScale = originalScale;
        AnimateBar();
    }

    public void StopBar()
    {
        LeanTween.cancel(this.gameObject);
    }

    public void ShowBar() {
        this.GetComponentInParent<Canvas>().enabled = true;
    }

    public void HideBar()
    {
        this.GetComponentInParent<Canvas>().enabled = false;
    }

    public void EndOfGame()
    {
        Debug.Log("in End of game");
        StopBar();
        HideBar();
        //ResetBar();
    }

    private void setColorCallback(Color c)
    {
        gameObject.GetComponent<Image>().color = c;
        

        // For some reason it also tweens my image's alpha so to set alpha back to 1 (I have my color set from inspector). You can use the following

        var tempColor = gameObject.GetComponent<Image>().color;
        tempColor.a = 1f;
        gameObject.GetComponent<Image>().color = tempColor;
    }
}
