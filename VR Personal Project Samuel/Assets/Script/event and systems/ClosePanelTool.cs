using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosePanelTool : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClosePanel(GameObject objectToClose)
    {
        objectToClose.SetActive(false);
    }

    public void OpenPanel(GameObject objectToOpen)
    {
        objectToOpen.SetActive(true);
    }

}
