using System.Collections;
using System;
using UnityEngine;

public class TreeManager : MonoBehaviour
{
    public GameManagerWoodCutting gameManagerRef;
    public float yOfsetOfChild = 0;
    public GameObject bottomLog;
    public GameObject topLog;
    [SerializeField]
    private Transform originalBottomLogPositionRef;

    [SerializeField]
    private GameObject _playerRef;
    // Start is called before the first frame update
    public int createdChildNumber = 0;
    public GameObject[] logPrefab;
    public int wantedNumberLog =4;
    
    // Boolen that indicate if the AXE can interact with the log (reset when the axe move away) 
    public bool canInteractWithTree = true;
    private GameObject lastInteractingObject;
    // private bool gameIsStarted = false;
    [SerializeField]
    private bool isTutorial = false;
    [SerializeField]
    private int tutorialLogToUse = 1;
    

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
        GameEvents.onGameReset += ResetGame;
    }

    public void OnDisable()
    {
        GameEvents.current.onTimerOver -= TimeOverAdapter;
        GameEvents.onGameReset -= ResetGame;
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

    public void ChildWasHitManager(bool goodSide, GameObject ColiderObject)
    {
        if(ColiderObject != null)
        {
            lastInteractingObject = ColiderObject;
        }

        //prevent the game from starting if required.
        if (gameManagerRef.gameCanStart == false)
        {
            Debug.Log("Can start the game yet");
            return;
        }

        if (gameManagerRef.gameIsStarted == false && goodSide == false)
        {
            Debug.Log("Can't start the game because you hit the wrong side");
            SetCanInteractWithTree(false);
            //Update the transfom the axe need to move away to reset.
            lastInteractingObject.GetComponentInChildren<AxeAwayFromTree>().updateTreeToInteractWith(originalBottomLogPositionRef,this);
            return;
        }

        if (gameManagerRef.gameIsStarted == false && goodSide == true)
        {
            GameEvents.current.GameIsStarted();
            if (isTutorial)
            {
                GameEvents.current.StopTimer();
            }
            //gameIsStarted = true;
        }
        
        ReplaceBottomChild(goodSide);
    }

    private void ReplaceBottomChild(bool goodSide)
    {
       
        
        // [Objective] : Prevent te player to keep interacting with the tree.
        //target the AXE
        SetCanInteractWithTree(false);
        lastInteractingObject.GetComponentInChildren<AxeOutlineManager>().setDeactivatedOutline();
        //Update the transfom the axe need to move away to reset.
        lastInteractingObject.GetComponentInChildren<AxeAwayFromTree>().updateTreeToInteractWith(originalBottomLogPositionRef,this);

        // get the bottom log section and get the parent log script
        ParentLog currentBottomLogRef = bottomLog.GetComponent<ParentLog>();
        //check if not null
        if(currentBottomLogRef != null)
        {
            ScreenUILogSystem.Instance.LogMessageToTreeUI("In replace Bottom Child");
            // [Objective] : Get a reference to the log that will be the next a the bottom.
            GameObject nextBottomLog = null;
            try
            {
                // your code segment which might throw an exception
                nextBottomLog = currentBottomLogRef.topNeighbourAnchor.transform.parent.gameObject;
            }
            catch (Exception ex)
            {
                Debug.LogException(ex, this);
            }
            
            nextBottomLog.GetComponent<ParentLog>().bottomNeighbourAnchor = null;

            // [Objective] : Removing the log that was hit.
            currentBottomLogRef.RemoveObjectFromScene(goodSide, isTutorial);
            
            //Check if the wrong side is hit and the game would finish.
            if(goodSide == false  && StatTraking.current.GetLifeRemaining() == 0)
            {
                //we filter the hit that would lead to a game over
                topLog = null;

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

    private void ReplaceLogTutorial(bool goodSide)
    {


        // [Objective] : Prevent te player to keep interacting with the tree.
        //target the AXE
        SetCanInteractWithTree(false);
        lastInteractingObject.GetComponentInChildren<AxeOutlineManager>().setDeactivatedOutline();

        // get the bottom log section and get the parent log script
        ParentLog currentBottomLogRef = bottomLog.GetComponent<ParentLog>();
        //check if not null
        if (currentBottomLogRef != null)
        {
            ScreenUILogSystem.Instance.LogMessageToTreeUI("In replace Bottom Child");
            // [Objective] : Get a reference to the log that will be the next a the bottom.
            GameObject nextBottomLog = null;
           /*  
            *  there will never be a next log
            try
            {
                // your code segment which might throw an exception
                nextBottomLog = currentBottomLogRef.topNeighbourAnchor.transform.parent.gameObject;
            }
            catch (Exception ex)
            {
                Debug.LogException(ex, this);
            }
           

            nextBottomLog.GetComponent<ParentLog>().bottomNeighbourAnchor = null;
           */
            // [Objective] : Removing the log that was hit.
            currentBottomLogRef.RemoveObjectFromScene(goodSide, isTutorial);

            //Check if the wrong side is hit and the game would finish.
            if (goodSide == false && StatTraking.current.GetLifeRemaining() == 0)
            {
                //we filter the hit that would lead to a game over
                topLog = null;

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
        lastInteractingObject.GetComponentInChildren<ChangeAxeCrackVisual>().IncreaseCrackVisual();
        ReplaceBottomChild(false);
    }

    private void ToExecuteAfterFinishingTweening()
    {
        bottomLog.GetComponent<ParentLog>().SetAsActiveLog();
        //[To do : call InitialiseLog() ]
        if (isTutorial)
        {
            bottomLog.GetComponent<MeshRenderer>().enabled = true;
        }
        bottomLog.GetComponent<ParentLog>().InitialiseLog();
        CreateNextTopChild();
    }
    /// <summary>
    /// Method call to create a new log on top of the tree.
    /// </summary>
    public void CreateNextTopChild()
    {
       // bool canCreateObject = true;
        //To-DO add a usefull check to know if you can still create a new log
        if (gameManagerRef.canCrateNewLog)
        {
            // [Objective] : Creating a new Log object and changing its name.
            string newName = "Log number " + createdChildNumber;
            createdChildNumber++;
            GameObject newLog = SelectAndInstatiateNewLog();
            if (isTutorial)
            {
                newLog.GetComponent<MeshRenderer>().enabled = false;
            }
            newLog.transform.name = newName;
           
            // Set up the  top and botom anchor link.
            ParentLog newLogScriptRef = newLog.GetComponent<ParentLog>();
            newLogScriptRef.SetUpObjectRef();

            //  On the new log, Set up the reference to their bottom neighbourg and set the Y ofset.
            ParentLog topLogParentScriptRef = topLog.GetComponent<ParentLog>();
            newLogScriptRef.bottomNeighbourAnchor = topLogParentScriptRef.getTopLink();
            newLogScriptRef.SetYOfsetPos(yOfsetOfChild);

            //Link the old top log to the newly created log and update top Log.
            topLogParentScriptRef.topNeighbourAnchor = newLogScriptRef.getBottomLink();
            topLog = newLog;


        }
        else
        {
            VRDebugConsol.Instance.LogMessageToConsol("Can't create new log");
        }
    }


    private void ResetGame()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        StartCoroutine(WaitForTreeCreationToBeEnabled());
    }
    public IEnumerator WaitForTreeCreationToBeEnabled()
    { 
        if(gameManagerRef.canCrateNewLog == false)
        {
            yield return null;

        }
        setUpNewTree();
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
        DeactivateOutlineIfPLayerIsAway();
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
        
        int logIntToCreate = 1;
        if(isTutorial == false) { 
            switch (gameManagerRef.GetGameLevel())
            {
                case GameManagerWoodCutting.gameLevel.start:
                    logIntToCreate = UnityEngine.Random.Range(1, 3);
                    gameManagerRef.IncreaseLevel();
                    break;
                case GameManagerWoodCutting.gameLevel.one:
                    logIntToCreate = UnityEngine.Random.Range(1, 3);
                    break;
                case GameManagerWoodCutting.gameLevel.two:
                    logIntToCreate = UnityEngine.Random.Range(0, 3);
                    break;
                case GameManagerWoodCutting.gameLevel.tree:
                    logIntToCreate = UnityEngine.Random.Range(0, 3);
                    break;
                default:
                    break;
            }
        }
        else
        {
            logIntToCreate = tutorialLogToUse;
        }
        //[To DO : add a check to prefent the first log to be solid]

        //[To Do : add a way to control wich log a created depending on the "Level"]

        //int logIntToCreate = UnityEngine.Random.Range(1, 3);
        ChildLog.ActiveChildSide logTypeToCreate = (ChildLog.ActiveChildSide)logIntToCreate;
        GameObject newLog = null;
        switch (logTypeToCreate)
        {
            case ChildLog.ActiveChildSide.None:
                newLog = Instantiate(logPrefab[0]);
                break;
            case ChildLog.ActiveChildSide.Left:
                newLog = Instantiate(logPrefab[1]);
                //return newLog;
                break;
            case ChildLog.ActiveChildSide.Right:
                newLog = Instantiate(logPrefab[2]);
                
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

    public void ActivateBottomLogOutline()
    {
        if(bottomLog.GetComponent<outlineManagment>() != null)
        {
            bottomLog.GetComponent<outlineManagment>().ActivateOutline();
        }
    }

    public void DeactivateOutlineIfPLayerIsAway()
    {
        if(Vector3.Distance(this.transform.position , _playerRef.transform.position) >= 1)
        {
            if(bottomLog != null)
            {
                bottomLog.GetComponent<outlineManagment>().DeactivateOutline();
            }
        }
    }

   
}
