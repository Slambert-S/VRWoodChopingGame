using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class activateRay : MonoBehaviour
{
    public UnityEngine.XR.Interaction.Toolkit.Interactors.XRRayInteractor rayInteractor;
    // Start is called before the first frame update
    public void OnEnable()
    {

        GameEvents.current.onGameOver += activeRayWithGrabedObject;
    }

    // Update is called once per frame

    private void activeRayWithGrabedObject()
    {
        rayInteractor.enabled = true;
       
    }
}
