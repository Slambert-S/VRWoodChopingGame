using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentLog : MonoBehaviour
{
    //Section for Value used
    private Vector3 angleForLeftLog = new Vector3(-90.0f, 0, 180f);
    private Vector3 angleForRightLog = new Vector3(-90.0f, 0, 0);
    /// Section used for detecting a colisions
    [SerializeField]
    private ChildLog.ActiveChildSide activeSide;
    private bool firstCollisionDetected = false;
    private Vector3 hitDirection;
    [SerializeField]
    private bool isActiveBottomLog = false;

    // Section used to handle mouvement and destruction
    [SerializeField]
    public Transform topNeighbourAnchor;
    [SerializeField]
    public Transform bottomNeighbourAnchor;

    [SerializeField]
    private Transform objectTopAnchorRef;
    [SerializeField]
    private Transform objectBottomAnchorRef;
    private bool isLaunched =false;
    private Vector3 positionBeforeLaunch;
   


    [SerializeField]
    private float yPosOfset = 0;


    // Start is called before the first frame update
    void Start()
    {
        SetUpObjectRef();
    }

    // Update is called once per frame
    void Update()
    {
        // LinkNeighbour();
        if (isLaunched)
        {
            Debug.Log(Vector3.Distance(this.transform.position, positionBeforeLaunch));
            if(Vector3.Distance(this.transform.position, positionBeforeLaunch) >= 1)
            {
                this.gameObject.GetComponent<BoxCollider>().isTrigger = false;
            }
        }
    }
    private void LateUpdate()
    {
        if (bottomNeighbourAnchor != null)
        {
            this.transform.position = bottomNeighbourAnchor.position;
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + yPosOfset, this.transform.position.z);
        }
    }

    /// <summary>
    /// Method Called to set up the Top and bottom anchor of the object
    /// </summary>
    public void SetUpObjectRef()
    {
        objectTopAnchorRef = gameObject.transform.Find("Top Link").gameObject.transform;
        objectBottomAnchorRef = gameObject.transform.Find("Bottom Link").gameObject.transform;
    }

    /// <summary>
    /// Methode called at the start of the scene to allow all log that are already present to find their neighbour and their Y ofset.
    /// </summary>
    /// <param name="newYOfset">The distance between that will be kept between each log</param>
    public void GameStartSetUp(float newYOfset)
    {
        LinkNeighbour();
        yPosOfset = newYOfset;
    }

    /// <summary>
    /// Methode that get called by the [ChildLog] script. Execute check to see if the log is active.
    /// If it is, the script check it the log was hit on their active side and record the angle of the hit.
    /// Communicate the information by calling a methode in the script [TreeManager] of the parent.
    /// </summary>
    /// <param name="sideWhoGotHit"></param>
    /// <param name="DirectionOfHit"></param>
    public void LearnWhatChildWasHit(ChildLog.ActiveChildSide sideWhoGotHit, Vector3 DirectionOfHit)
    {
        // Check To prevent colliding with log that are not the active one
        if(isActiveBottomLog == false)
        {
            return;
        }
        if(GetComponentInParent<TreeManager>().canInteractWithTree == false)
        {
            return;
        }

        //Make sure to not activate the script multiple time in one swing
        bool goodSideHit = false;
        if (!firstCollisionDetected)
        {
            firstCollisionDetected = true;

            if(sideWhoGotHit == activeSide)
            {
                ScreenUILogSystem.Instance.LogMessageToTreeUI(sideWhoGotHit.ToString() + " : Good");
                hitDirection = DirectionOfHit;
                goodSideHit = true;
            }
            else
            {
                ScreenUILogSystem.Instance.LogMessageToTreeUI(sideWhoGotHit.ToString() + " : Wrong");
                goodSideHit = false;
            }
            gameObject.GetComponentInParent<TreeManager>().ReplaceBottomChild(goodSideHit);
        }
    }


    //raycast to link object to their top and bottom neighbour
    public void LinkNeighbour()
    {
        //Debug.Log("pepiou");
        // Bit shift the index of the layer (8) to get a bit mask
         int layerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        // layerMask = ~layerMask;

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity,layerMask))
        {
            Debug.Log(hit.collider.transform.name, hit.collider.gameObject);
            if (hit.collider.CompareTag("Log"))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                //Debug.Log(hit.collider.gameObject.transform.name);
                //link the anchor point belonging to the neighbour on top
                topNeighbourAnchor = hit.collider.gameObject.GetComponent<ParentLog>().getBottomLink();
            }

        }

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), out hit, Mathf.Infinity,layerMask))
        {
            if (hit.collider.CompareTag("Log"))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.back) * hit.distance, Color.yellow);
                //Debug.Log(hit.collider.gameObject.transform.name);
                //link the anchor point belonging to the neighbour on top
                bottomNeighbourAnchor = hit.collider.gameObject.GetComponent<ParentLog>().getTopLink();
            }
        }

    }

    /// <summary>
    /// Method called to remove the log from the tree and the scene.
    /// The impulse direction will change depending on the active side of the log.
    /// </summary>
    /// <param name="goodSide"> Value that indicate it the log was hit on their active side </param>
    public void RemoveObjectFromScene(bool goodSide)
    {
        if(goodSide) //IF he log is hit on the good side
        {
            //Objective : add component required for physics and push the log in a specific direction
            this.gameObject.AddComponent<Rigidbody>();
            this.gameObject.GetComponent<Rigidbody>().useGravity = true;
            Vector3 ImpulsDirection = new Vector3(6, 0, 0);

            // Change the direction of the impuls depending on the type of log that was hit.
            if (activeSide == ChildLog.ActiveChildSide.Right)
            {
                ImpulsDirection *= -1;
            }
            positionBeforeLaunch = this.transform.position;
            isLaunched = true;
            this.gameObject.GetComponent<Rigidbody>().AddForce(ImpulsDirection, ForceMode.Impulse);

            //We want to destroy the cube
            StartCoroutine(TotalDestruction(0.75f));
            StatTraking.current.IncreasGoodLogHit();

        }
        else //IF the log is hit on the wrong side
        {

            LeanTween.scale(this.gameObject, new Vector3(0, 0, 0), 0.25f);
            LeanTween.moveLocalY(this.gameObject, this.gameObject.transform.position.y - 1, 0.25f);
            StartCoroutine(TotalDestruction(0.75f));
            StatTraking.current.IncreasBadLogHit();
            StatTraking.current.RemoveLife();
        }
        GameEvents.current.LogIsRemoved();

    }

    /// <summary>
    /// Methode call to apply and change required when a log is created.
    /// Currently only change the angle of the log.
    /// May add change of shader
    /// </summary>
    public void SetUpObjectAfterCreation()
    {
        switch (activeSide)     
        {
            case ChildLog.ActiveChildSide.None:
                break;
            case ChildLog.ActiveChildSide.Left:
                this.gameObject.transform.localEulerAngles = angleForLeftLog;
                break;
            case ChildLog.ActiveChildSide.Right:
                this.gameObject.transform.localEulerAngles = angleForRightLog;
                break;
            default:
                break;
        }
    }

        /// <summary>
        ///  Call when you want to get a reference to the top or bottom link anchor of the object
        /// </summary>
    public Transform getTopLink()
    {

        return objectTopAnchorRef;
    }
    public Transform getBottomLink()
    {

        return objectBottomAnchorRef;
    }

    public void SetYOfsetPos(float newYOfset)
    {
        yPosOfset = newYOfset;
    }

    public void SetAsActiveLog()
    {
        isActiveBottomLog = true;
        //SetUpShader for active log
    }

    
    public IEnumerator TotalDestruction(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }


}
