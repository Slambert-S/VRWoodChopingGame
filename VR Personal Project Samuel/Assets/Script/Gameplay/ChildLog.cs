using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildLog : MonoBehaviour
{
    public ActiveChildSide childSide;
    private ParentLog parentRef;

    // Start is called before the first frame update
    void Start()
    {
        parentRef = this.GetComponentInParent<ParentLog>();
    }

    public void OnTriggerEnter(Collider other)
    {
        ScreenUILogSystem.Instance.LogMessageToTreeUI(this.gameObject.name +" of : "+this.gameObject.transform.parent.name + " Was hit; out");
        //check if its the axe head part
        if (other.name == "axe head")
        {
            ScreenUILogSystem.Instance.LogMessageToTreeUI(this.gameObject.name + " of : " + this.gameObject.transform.parent.name + " Was hit; in");
            //destination - current
            Vector3 dirrectionOfHIt = (this.transform.position - other.gameObject.transform.position).normalized;
            TellParentHitWasDetected(dirrectionOfHIt);
        }

        
        /// if yes -> call function in the parent script
    }

    private void TellParentHitWasDetected( Vector3 directionOfhit)
    {
        parentRef.LearnWhatChildWasHit(childSide, directionOfhit);
    }
    public enum ActiveChildSide { None,Left,Right};
}
