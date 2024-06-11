using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class demoDavidScript : MonoBehaviour
{
    
    public GameObject player;
    public GameObject newPosition;
    

    public void MovePlayer()
    {
        player.transform.position = newPosition.transform.position;
        player.transform.rotation = newPosition.transform.rotation;
    }
}
