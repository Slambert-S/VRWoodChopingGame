using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TreeManager : MonoBehaviour
{
    public GameManagerWoodCutting gameManagerRef;
    public float yOfsetOfChild = 0;
    public GameObject bottomLog;
    public GameObject topLog;
    [SerializeField]
    private Transform originalBottomLogPositionRef;
    // Start is called before the first frame update
    public int createdChildNumber = 0;
    public GameObject[] logPrefab;
    public int wantedNumberLog =4;
    
    public bool canInteractWithTree = true;
   // private bool gameIsStarted = false;

    [Header("Debug control")]
    public bool startTreeEmpty = false;
    void Start()
    {
        if (startTreeEmpty == false)
        {
            SetUpAllChild();
            this.bottomLog = this.transform.GetChild(0).gameObject;
            this.topLog = this.transform.GetChild(this.transform.childCount - 1).gameObject;
            
        }
        else
        {
            setUpNewTree();

        }

       
    }

    public void OnEnable()
    {
       // GameEvents.current.onTimerOver += TimeOverAdapter;
       /*if(GameEvents.current != null)
        {
            GameEvents.onGameReset += setUpNewTree;

        }
        else
        {
            Debug.Log("Game event is nopt yet initialised");
        }*/

        GameEvents.current.onTimerOver += TimeOverAdapter;
        GameEvents.onGameReset += setUpNewTree;
    }

    public void OnDisable()
    {
        GameEvents.current.onTimerOver -= TimeOverAdapter;
        GameEvents.onGameReset -= setUpNewTree;
    }

    private void SetUpAllChild()
    {
        foreach (Transform child in transform)
        {
            ParentLog parentLogScriptRef = child.GetComponent<ParentLog>();
            if (parentLogScriptRef != null)
            {
                parentLogScriptRef.SetUpObjectRef();

            }
        }
        foreach (Transform child in transform)
        {
            ParentLog parentLogScriptRef = child.GetComponent<ParentLog>();
            if(parentLogScriptRef != null)
            {
                parentLogScriptRef.GameStartSetUp(yOfsetOfChild);
                Debug.Log(" child found");

            }
        }
    }

    public void ReplaceBottomChild(bool goodSide)
    {
        //prevent the game from starting if required.
        if(gameManagerRef.gameCanStart == false)
        {
            Debug.Log("Can start the game yet");
            return;
        }

        if(gameManagerRef.gameIsStarted == false && goodSide == false)
        {
            Debug.Log("Can start the game because you hit the wrong side");
            SetCanInteractWithTree(false);
            return;
        }

        if (gameManagerRef.gameIsStarted == false && goodSide == true )
        {
            GameEvents.current.GameIsStarted();
            //gameIsStarted = true;
        }
        
        // [Objective] : Prevent te player to keep interacting with the tree.
        //target the AXE
        SetCanInteractWithTree(false);

        // get the bottom log section and get the parent log script
        ParentLog currentBottomLogRef = bottomLog.GetComponent<ParentLog>();
        //check if not null
        if(currentBottomLogRef != null)
        {
            ScreenUILogSystem.Instance.LogMessageToTreeUI("In replace Bottom Child");
            // [Objective] : Get a reference to the log that will be the newxt a the bottom.
            GameObject nextBottomLog = currentBottomLogRef.topNeighbourAnchor.transform.parent.gameObject;
            nextBottomLog.GetComponent<ParentLog>().bottomNeighbourAnchor = null;

            // [Objective] : Removing the log that was hit.
            currentBottomLogRef.RemoveObjectFromScene(goodSide);
            
            //Check if the wrong side is hit and the game would finish.
            //if => 
            if(goodSide == false  && StatTraking.current.GetLifeRemaining() == 0)
            {
                //we filter the hit that would lead to a game over
            }
            else
            {
                //This part is simply to move the stack of log down.

                bottomLog = nextBottomLog;
                // [Objective] : move the tree down
                LeanTween.move(bottomLog, originalBottomLogPositionRef, 0.25f).setOnComplete(ToExecuteAfterFinishingTweening);
                //CreateNextTopChild();

            }

            //Separate the last hit



        }

    }

    private void TimeOverAdapter()
    {
        ReplaceBottomChild(false);
    }

    private void ToExecuteAfterFinishingTweening()
    {
        bottomLog.GetComponent<ParentLog>().SetAsActiveLog();
        CreateNextTopChild();
    }
    /// <summary>
    /// Method call to create a new log on top of the tree.
    /// </summary>
    public void CreateNextTopChild()
    {
        bool canCreateObject = true;
        //To-DO add a usefull check to know if you can still create a new log
        if (canCreateObject)
        {
            // [Objective] : Creating a new Log object and changing its name.
            string newName = "Log number " + createdChildNumber;
            createdChildNumber++;
            GameObject newLog = SelectAndInstatiateNewLog();
            newLog.transform.name = newName;
           
            // [Objective] :  Set up the anchor link.
            ParentLog newLogScriptRef = newLog.GetComponent<ParentLog>();
            newLogScriptRef.SetUpObjectRef();

            // [Objective] : Set up the the reference to the bottom neighbourg and set the Y ofset.
            ParentLog topLogParentScriptRef = topLog.GetComponent<ParentLog>();
            newLogScriptRef.bottomNeighbourAnchor = topLogParentScriptRef.getTopLink();
            newLogScriptRef.SetYOfsetPos(yOfsetOfChild);

            // [Objective] : Link the old top log to the newly created log and update top Log.
            topLogParentScriptRef.topNeighbourAnchor = newLogScriptRef.getBottomLink();
            topLog = newLog;


        }
    }

    //Set up new tree objective
    /*
     * Creat a new log in the bottom possition.
     * Set the new log as top and bottom of the tree stack.
     * Creat the next top child X(right now 4) time 
     * 
     * 
     * */

    public void setUpNewTree()
    {
        Debug.Log("in SetUpNewTree");
        createdChildNumber = 0;
        string newName = "Log number " + createdChildNumber;
        createdChildNumber++;
        GameObject newLog = SelectAndInstatiateNewLog();
        newLog.transform.name = newName;

        // [Objective] :  Set up the anchor link.
        ParentLog newLogScriptRef = newLog.GetComponent<ParentLog>();
        newLogScriptRef.SetUpObjectRef();
        topLog = newLog;
        bottomLog = newLog;
        bottomLog.gameObject.GetComponent<ParentLog>().SetAsActiveLog();
        for (int i = 1; i < wantedNumberLog; i++)
        {
            CreateNextTopChild();
        }

    }

    public void SetCanInteractWithTree(bool newValue)
    {
        canInteractWithTree = newValue;
    }
    /// <summary>
    /// Create a new random Log GameObject and handle general set up of the object.
    /// </summary>
    /// <returns> Return a reference to the new Log created</returns>
    private GameObject SelectAndInstatiateNewLog()
    {
        int logIntToCreate = Random.Range(1, 3);
        ChildLog.ActiveChildSide logTypeToCreate = (ChildLog.ActiveChildSide)logIntToCreate;
        GameObject newLog = null;
        switch (logTypeToCreate)
        {
            case ChildLog.ActiveChildSide.None:
                break;
            case ChildLog.ActiveChildSide.Left:
                newLog = Instantiate(logPrefab[0]);
                //return newLog;
                break;
            case ChildLog.ActiveChildSide.Right:
                newLog = Instantiate(logPrefab[1]);
                
                //return newLog;
                break;
            default:
                break;
        }

        if(newLog != null)
        {
            if(topLog != null)
            {
                newLog.transform.position = topLog.transform.position;
            }
            else
            {
                newLog.transform.position = originalBottomLogPositionRef.position;
            }
            newLog.transform.parent = this.gameObject.transform;
            ParentLog reference = null;
            try
            {
                reference = newLog.GetComponent<ParentLog>();
            }
            catch (System.Exception)
            {

                throw;
            }
             
            if(reference != null)
            {
                newLog.GetComponent<ParentLog>().SetUpObjectAfterCreation();

            }
            else
            {
                newLog.GetComponentInChildren<ParentLog>().SetUpObjectAfterCreation();
            }
            return newLog  ;
        }
        else
        {
            return null;
        }

    }
}
