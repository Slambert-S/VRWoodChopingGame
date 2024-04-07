using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ScreenUILogSystem : MonoBehaviour
{
    public static ScreenUILogSystem Instance { get; private set; }
    // Start is called before the first frame update
    public TMP_Text _ConsolText;

    public TMP_Text _TreeUIText;

    public List<String> ListOflog;

    public List<String> ListOfLogTree;

    public int maxListOfLogSize = 5;
    private int messageId = 0;

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

    public void LogMessageToUI(String messageToLog)
    {

        if (ListOflog.Count >= maxListOfLogSize)
        {
            ListOflog.RemoveAt(0);
        }
        ListOflog.Add(messageToLog);
        String tempText = "Console log  :\t";
        foreach(String  sentence in ListOflog)
        {
            tempText += "\n\t\t : " + sentence;
        }
        _ConsolText.text = tempText;
       // Debug.Log(messageToLog);
    }

    public void LogMessageToTreeUI(String messageToLog)
    {

        if (ListOfLogTree.Count >= maxListOfLogSize)
        {
            ListOfLogTree.RemoveAt(0);
        }
        ListOfLogTree.Add(messageToLog);
        String tempText = "Console log : "+messageId + " :\t";
        foreach (String sentence in ListOfLogTree)
        {
            tempText += "\n\t\t : " + sentence;
        }
        _TreeUIText.text = tempText;
        messageId++;
        // Debug.Log(messageToLog);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
