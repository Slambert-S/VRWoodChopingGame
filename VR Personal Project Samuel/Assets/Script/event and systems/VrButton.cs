using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class VrButton : MonoBehaviour
{
    //Time that the button is set inactive after release.
    public float deadTime = 1.0f;
    //Bool used to lock down button duering its set dead time.
    private bool _deadTimeActive = false;

    public UnityEvent onPressed, onReleased;


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Button" && !_deadTimeActive)
        {
            onPressed?.Invoke();
            Debug.Log("I have been press");
        }
    }
    //Check if the current collider exiting is the button and set off OnRelease event
    //Also call the function to lock the button
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Button" && !_deadTimeActive)
        {
            onReleased?.Invoke();
            Debug.Log("I have been released");
            StartCoroutine(WaitForDeadTime());
        }
    }

    //Lock the button for a period of time.
    IEnumerator WaitForDeadTime()
    {
        _deadTimeActive = true;
        yield return new WaitForSeconds(deadTime);
        _deadTimeActive = false;
    }
}
