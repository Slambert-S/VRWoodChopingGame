using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Attachment;

public class SocketChangeGrabPoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangGrabPositionForSocketHover(UnityEngine.XR.Interaction.Toolkit.HoverEnterEventArgs args)
    {
        //Debug.Log(args.interactableObject.transform.name);
        args.interactableObject.transform.gameObject.GetComponentInChildren<changeAttachPoint>().changeGrabPoint(true);
    }
    public void ChangGrabPositionForSocketSelect(UnityEngine.XR.Interaction.Toolkit.SelectEnterEventArgs args)
    {
        //Debug.Log(args.interactableObject.transform.name);
        //this.GetComponent<UnityEngine.XR.Interaction.Toolkit.XRSocketInteractor>();
        
        args.interactableObject.transform.gameObject.GetComponentInChildren<changeAttachPoint>().changeGrabPoint(true);
    }
    public void ChangGrabPositionForGrabHover(UnityEngine.XR.Interaction.Toolkit.HoverExitEventArgs args)
    {
       // Debug.Log(args.interactableObject.transform.name);
        args.interactableObject.transform.gameObject.GetComponentInChildren<changeAttachPoint>().changeGrabPoint(false);
    }
    public void ChangGrabPositionForGrabSelect(UnityEngine.XR.Interaction.Toolkit.SelectExitEventArgs args)
    {
        Debug.Log(args.interactableObject.transform.name);
        args.interactableObject.transform.gameObject.GetComponentInChildren<changeAttachPoint>().changeGrabPoint(false);
    }
}
