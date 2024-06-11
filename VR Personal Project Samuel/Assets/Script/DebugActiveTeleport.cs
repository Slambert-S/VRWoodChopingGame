using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugActiveTeleport : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CallOnActivate()
    {
        Debug.Log(" this is activited");
    }

    public void CallOnDeactivate()
    {
        Debug.Log(" this is deactivated");
    }
}
