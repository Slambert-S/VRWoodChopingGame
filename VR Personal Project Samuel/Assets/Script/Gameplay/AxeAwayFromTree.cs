using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeAwayFromTree : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private TreeManager treeScriptRef;
    public Transform positionToMoveAwayFrom;
    
    // Update is called once per frame
    void Update()
    {
        AcivateAxeInteraction();
    }

    public void updateTreeToInteractWith(Transform newposition, TreeManager treeManager)
    {
        //[INFO] Uptade the reference use to select the tree you just interacted with and the position to move away from
        treeScriptRef = treeManager;
        positionToMoveAwayFrom = newposition;
       // Debug.LogWarning("In updateTreeToInteract");
    }
   
    private void AcivateAxeInteraction()
    {
        float distance = Vector3.Distance(this.transform.position, positionToMoveAwayFrom.position);
        
        //Debug.Log(distance);
        if(treeScriptRef == null)
        {
            Debug.Log("TreeScriptRef In AxeAwayFromTree is not set");
            return;
        }
        //[Info] Check if the axe far away from the tree and activate the interaction if it is.
        if( distance >= 0.75 && treeScriptRef.canInteractWithTree == false)
        {
           // Debug.LogWarning("Axe is far away. Can interact with tree :" + treeScriptRef.canInteractWithTree + " || Distance :" + distance);
            treeScriptRef.SetCanInteractWithTree(true);
            //change color of the outline
            this.GetComponent<AxeOutlineManager>().setActiveOultine();
        }
    }
}
