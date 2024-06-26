using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeAwayFromTree : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private TreeManager treeScriptRef;
    public Transform positionToMoveAwayFrom;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        AcivateAxeInteraction();
    }

    public void updateTreeToInteractWith(Transform newposition, TreeManager treeManager)
    {
        treeScriptRef = treeManager;
        positionToMoveAwayFrom = newposition;
    }
   /* private void OnTriggerEnter(Collider other)
    {
        ScreenUILogSystem.Instance.LogMessageToTreeUI("Collisions detected");
        if (other.name == "axe head")
        {
            treeScriptRef.SetCanInteractWithTree(true);
        }
    }*/

    private void AcivateAxeInteraction()
    {
        float distance = Vector3.Distance(this.transform.position, positionToMoveAwayFrom.position);
       // VRDebugConsol.Instance.LogMessageToConsol("Distance value: " + distance);
      //  Debug.Log("[DEBUG] Distance value : " + distance);
        if(treeScriptRef == null)
        {
            Debug.Log("TreeScriptRef In AxeAwayFromTree is not set");
            return;
        }
        if( distance >= 0.75 && treeScriptRef.canInteractWithTree == false)
        {
            treeScriptRef.SetCanInteractWithTree(true);
            //change color of the outline
            this.GetComponent<AxeOutlineManager>().setActiveOultine();
        }


    }
}
