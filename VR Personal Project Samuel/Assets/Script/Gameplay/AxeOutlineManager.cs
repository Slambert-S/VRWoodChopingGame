using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeOutlineManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Color activeColor;
    [SerializeField] 
    private Color deactivatedColor;
    [SerializeField]
    private GameObject objectWithOutline;

    private Material outlineMaterial;

    void Start()
    {
        if(objectWithOutline == null)
        {
            Debug.Log("objectWithOultine is not assigned in : AxeOutlineManager");
        }
        else
        {
            foreach (Material matt in objectWithOutline.GetComponent<Renderer>().materials)
            {
                Debug.Log("[Debug]  Material name :" + matt.name);
                if(matt.name == "Outline mt (Instance)")
                {
                    outlineMaterial = matt;
                    Debug.Log("[Debug] This is the correct material");
                }
            }
        }
    }

    // Update is called once per frame
  
    public void setActiveOultine()
    {
        if(outlineMaterial != null)
        {
            outlineMaterial.SetColor("_Outline_Color", activeColor);
        }
    }
    public void setDeactivatedOutline()
    {

        if (outlineMaterial != null) 
        {
            Debug.Log("Should change color to deactivated");
            outlineMaterial.SetColor("_Outline_Color", deactivatedColor);
        }
        else
        {
            Debug.LogWarning("Outline material is not set");
        }


    }
}
