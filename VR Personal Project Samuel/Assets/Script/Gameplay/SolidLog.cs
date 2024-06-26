using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolidLog : ParentLog
{
    [SerializeField]
    private float _timeToWait;
    private Vector3 _positionBeforeLaunch;
    private bool _isLaunched = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_isLaunched)
        {
            //  Debug.Log(Vector3.Distance(this.transform.position, positionBeforeLaunch));
            if (Vector3.Distance(this.transform.position, _positionBeforeLaunch) >= 1)
            {
                this.gameObject.GetComponent<BoxCollider>().isTrigger = false;
            }
        }
    }
     public override void InitialiseLog()
    {
        Debug.Log("In SolidLog : InitialiseLog()");
        StartCoroutine(StartCountDownBeforeLogClear());
    }

    public IEnumerator StartCountDownBeforeLogClear()
    {
        Debug.Log("In solid log : befor waiting");
        VRDebugConsol.Instance.LogMessageToConsol("In solid log : befor waiting");
        yield return new WaitForSeconds(_timeToWait);
       
       
        Debug.Log("In solid log : after waiting");
        VRDebugConsol.Instance.LogMessageToConsol("In solid log : after waiting");
        this.gameObject.GetComponentInParent<TreeManager>().ChildWasHitManager(true, null);
        //Send feedback upward to say the log was properly dealth with.
    }
    public override void StartCustomRemoveAnimation()
    {
        // base.StartCustomRemoveAnimation();
        //Objective : add component required for physics and push the log in a specific direction
        this.gameObject.AddComponent<Rigidbody>();
        this.gameObject.GetComponent<Rigidbody>().useGravity = true;
        Vector3 ImpulsDirection = new Vector3(6, 0, 0);

        // Change the direction of the impuls depending on the type of log that was hit.
       /* 
        if (activeSide == ChildLog.ActiveChildSide.Right)
        {
            ImpulsDirection *= -1;
        }
       */
        _positionBeforeLaunch = this.transform.position;
        _isLaunched = true;
        this.gameObject.GetComponent<Rigidbody>().AddForce(ImpulsDirection, ForceMode.Impulse);
    }
}
