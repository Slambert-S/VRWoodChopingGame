using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideObject : MonoBehaviour
{
    void OnEnable()
    {
        if (GameEvents.current == null)
        {
            Debug.LogWarning("GameEvent is not yet created");
        }
        else
        {
            GameEvents.current.onGameStart += HidePanel;
        }
    }
    void OnDisable()
    {
        if (GameEvents.current == null)
        {
            Debug.LogWarning("GameEvent is not yet created");
        }
        else
        {
            GameEvents.current.onGameStart -= HidePanel;
        }
    }

    private void HidePanel()
    {
        this.gameObject.SetActive(false);
    }

}
