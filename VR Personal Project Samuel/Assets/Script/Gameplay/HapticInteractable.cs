using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[System.Serializable]
public class Haptic
{
    [Range(0, 1)]
    public float intensity;
    public float duration;

    public void TriggerHaptic(BaseInteractionEventArgs EventArg)
    {
        if (EventArg.interactorObject is UnityEngine.XR.Interaction.Toolkit.Interactors.XRBaseInputInteractor controllerInteractor)
        {
            TriggerHaptic(controllerInteractor.xrController);
        }
    }

    public void TriggerHaptic(XRBaseController controller)
    {
        if (intensity > 0)
        {
            controller.SendHapticImpulse(intensity, duration);
        }
        //controller.
    }
}

public class HapticInteractable : MonoBehaviour
{
    public Haptic hapticOnActivated;
    public Haptic hapticSelectEntered;
    private UnityEngine.XR.Interaction.Toolkit.Interactors.XRBaseInputInteractor activatedController;
    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable>();
        interactable.activated.AddListener(SendFeedback);
        //interactable.selectEntered.AddListener(hapticSelectEntered.TriggerHaptic);
        interactable.selectEntered.AddListener(SaveController);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*
    public void TriggerHaptic(BaseInteractionEventArgs EventArg)
    {
        if(EventArg.interactorObject is XRBaseInputInteractor controllerInteractor)
        {
            TriggerHaptic(controllerInteractor.xrController);
        }
    }

    public void TriggerHaptic(XRBaseController controller)
    {
        if(intensity > 0)
        {
            controller.SendHapticImpulse(intensity, duration);
        }
        //controller.
    }
    */
    public void SaveController(BaseInteractionEventArgs EventArg)
    {
        if (EventArg.interactorObject is UnityEngine.XR.Interaction.Toolkit.Interactors.XRBaseInputInteractor controllerInteractor)
        {
            activatedController = controllerInteractor;
        }
        //EventArg.interactableObject.
    }

    public void SendFeedback(BaseInteractionEventArgs nada)
    {

        activatedController.SendHapticImpulse(0.5f, 0.5f);
        
        //controller.
    }

    public void SendFeedback( HapticType type)
    {
        
        float intensity = 0;
        float duration = 0;
        if(activatedController != null)
        {

            switch (type)
            {
                case HapticType.GoodSide:
                    intensity = 0.2f;
                    duration = 0.3f;
                    break;
                case HapticType.WrongSide:
                    intensity = 0.7f;
                    duration = 0.3f;
                    break;

            }                          
                activatedController.SendHapticImpulse(intensity, duration);
            

        }

        //controller.
    }

    public enum HapticType { GoodSide, WrongSide };



}
