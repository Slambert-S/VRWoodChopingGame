using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP_M_woodCutting : MonoBehaviour
{
    [SerializeField]
    private TreeManager _treeManagerRef;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateMiniGame()
    {
        _treeManagerRef.ActivateBottomLogOutline();
    }

    public void DeactivateMinigame()
    {
        _treeManagerRef.DeactivateOutlineIfPLayerIsAway();
    }
}
