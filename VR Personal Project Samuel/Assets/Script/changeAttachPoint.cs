using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Attachment;
using UnityEngine.XR.Interaction.Toolkit.Transformers;
using UnityEngine.XR.Interaction.Toolkit.Utilities;

using UnityEngine.XR.Interaction.Toolkit.Utilities.Pooling;

public class changeAttachPoint : MonoBehaviour
{
    public UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable reference;
    public Transform originalAttachPoint;
    public Transform SecondAttachPoint;
    public bool toogleTragink = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeGrabPoint(bool attachPointIsForSocket)
    {
        if (attachPointIsForSocket)
        {
            reference.attachTransform = SecondAttachPoint;
        }
        else
        {
            reference.attachTransform = originalAttachPoint;
        }
    }

    public void ChangGrabPositionForSocket(UnityEngine.XR.Interaction.Toolkit.HoverEnterEventArgs args)
    {
        Debug.Log(args.interactableObject.transform.name);
        args.interactableObject.transform.gameObject.GetComponentInChildren<changeAttachPoint>().changeGrabPoint(true);
    }
    public void ChangGrabPositionForGrab(UnityEngine.XR.Interaction.Toolkit.HoverEnterEventArgs args)
    {
        Debug.Log(args.interactableObject.transform.name);
        args.interactableObject.transform.gameObject.GetComponentInChildren<changeAttachPoint>().changeGrabPoint(false);
    }
}
