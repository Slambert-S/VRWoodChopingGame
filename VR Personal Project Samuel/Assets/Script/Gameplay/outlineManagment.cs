using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class outlineManagment : MonoBehaviour
{
    [SerializeField]
    private Color activeColor;
    [SerializeField]
    private Color deactivatedColor;

    [SerializeField]
    private float _activeOutlineSize = 0.0109f;
    public bool startWithVisibleOutline;

    [SerializeField]
    private GameObject objectWithOutline;

    private Material outlineMaterial;

    // Start is called before the first frame update
    void Start()
    {
        objectWithOutline = this.gameObject;
        if (objectWithOutline == null)
        {
            Debug.Log("objectWithOultine is not assigned in : AxeOutlineManager");
        }
        else
        {
            foreach (Material matt in objectWithOutline.GetComponent<Renderer>().materials)
            {
                ///Debug.Log("[Debug]  Material name :" + matt.name);
                if (matt.name == "Outline mt (Instance)")
                {
                    outlineMaterial = matt;
                    //Debug.Log("[Debug] This is the correct material");

                    if (startWithVisibleOutline)
                    {
                        ActivateOutline();
                    }
                    else
                    {
                        DeactivateOutline();
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateOutline()
    {
        if(outlineMaterial != null)
        {
            outlineMaterial.SetFloat("_Outline_Thicknes", _activeOutlineSize);
            SetActivateColor();
        }
    }

    public void DeactivateOutline()
    {
        if (outlineMaterial != null)
        {
           // Debug.Log("In Deactivate outline");
            outlineMaterial.SetFloat("_Outline_Thicknes", 0f);
            //SetActivateColor();
        }
    }

    public void SetActivateColor()
    {
        outlineMaterial.SetColor("_Outline_Color", activeColor);
    }

    public void SetDeactivateColor()
    {

    }
}
