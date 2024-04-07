using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingParentLog : MonoBehaviour
{
    // Start is called before the first frame update
    public TestingChildLog.ChildSide activeSide;
    public bool firstCollisionsDetected = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LearnWhatChildWasHit(TestingChildLog.ChildSide childSide)
    {
        if(firstCollisionsDetected == false)
        {
            firstCollisionsDetected = true;
            if(childSide == activeSide)
            {

                ScreenUILogSystem.Instance.LogMessageToUI(childSide.ToString()+ " Good");
            }
            else
            {
                ScreenUILogSystem.Instance.LogMessageToUI( "Wrong side");
            }
            Destroy(this.gameObject);
        }
        
    }
}
