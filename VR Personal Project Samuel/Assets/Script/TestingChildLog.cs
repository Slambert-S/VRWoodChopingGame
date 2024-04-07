using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingChildLog : MonoBehaviour
{
    // Start is called before the first frame update
    
    public ChildSide Side;
    private TestingParentLog parent;
    void Start()
    {
        parent = this.transform.parent.GetComponent<TestingParentLog>();
    }

 

    public void OnTriggerEnter(Collider other)
    {
        if(other.name == "axe head")
        {
            testCallSideActive();
            //ScreenUILogSystem.Instance.LogMessageToUI(other.name);
        }
       
    }
    public void testCallSideActive()
    {
        parent.LearnWhatChildWasHit(Side);
    }
    public enum ChildSide { Left, Right };
}
