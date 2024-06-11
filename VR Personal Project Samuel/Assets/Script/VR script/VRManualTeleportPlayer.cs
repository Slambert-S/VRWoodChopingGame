using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRManualTeleportPlayer : MonoBehaviour
{
    public GameObject playerRef;
    public GameObject newPosition;

    public void TeleportPlayer()
    {
        playerRef.transform.position = newPosition.transform.position;
        playerRef.transform.rotation = newPosition.transform.rotation;
    }
}
