using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeAwayFromTree : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private TreeManager treeScriptRef;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        ScreenUILogSystem.Instance.LogMessageToTreeUI("Collisions detected");
        if (other.name == "axe head")
        {
            treeScriptRef.SetCanInteractWithTree(true);
        }
    }
}
