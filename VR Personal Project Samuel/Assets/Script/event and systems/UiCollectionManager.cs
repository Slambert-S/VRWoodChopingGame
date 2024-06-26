using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiCollectionManager : MonoBehaviour
{
    public List<GameObject> uiGroup;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openSpecificUi(GameObject UiToOpen)
    {
        foreach (GameObject UiEllement in uiGroup)
        {
           
            if (GameObject.ReferenceEquals(UiEllement, UiToOpen))
            {
                UiEllement.SetActive(true);
            }
            else
            {
                UiEllement.SetActive(false);
            }
                

        }
    }
}
