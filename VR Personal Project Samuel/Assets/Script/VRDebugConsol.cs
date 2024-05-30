using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class VRDebugConsol : MonoBehaviour
{
    public static VRDebugConsol Instance { get; private set; }

    public TMP_Text _ConsolText;

    private List<String> ListOflog = new List<String>();

    public int maxListOfLogSize = 5;
    private int messageId = 0;
    // Start is called before the first frame update
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    public void LogMessageToConsol(String messageToLog)
    {

        if (ListOflog.Count >= maxListOfLogSize)
        {
            ListOflog.RemoveAt(0);
        }
        ListOflog.Add(messageToLog);
        String tempText = "Console log  :\t";
        foreach (String sentence in ListOflog)
        {
            tempText += "\n\t\t : " + sentence;
        }
        _ConsolText.text = tempText;
        // Debug.Log(messageToLog);
    }
}
