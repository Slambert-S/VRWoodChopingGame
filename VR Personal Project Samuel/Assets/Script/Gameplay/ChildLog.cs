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
            TellParentHitWasDetected(dirrectionOfHIt, other.gameObject);
            //other.gameObject.GetComponentInParent<HapticInteractable>().SendFeedback();
        }

        
        /// if yes -> call function in the parent script
    }

    private void TellParentHitWasDetected(Vector3 directionOfhit, GameObject collisionRef)
    {
        if (parentRef != null)
        {
            parentRef.SaveObjectToSendHapticTo(collisionRef.GetComponentInParent<HapticInteractable>());
            parentRef.LearnWhatChildWasHit(childSide, directionOfhit,collisionRef.transform.parent.gameObject);
        }
    }
    public enum ActiveChildSide { None,Left,Right};
}
