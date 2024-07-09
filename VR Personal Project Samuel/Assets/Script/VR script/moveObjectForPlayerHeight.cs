using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveObjectForPlayerHeight : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 StandingHeight;
    public Vector3 SittingHeight;

    public bool useLocalPotisiton = false;
    void Start()
    {
        if (useLocalPotisiton)
        {
            SittingHeight = this.gameObject.transform.localPosition;
        }
        else
        {
            SittingHeight = this.gameObject.transform.position;
        }
    }

    private void OnEnable()
    {
        if (GameEvents.current == null)
        {
            Debug.LogWarning("GameEvent is not yet created");
        }
        else
        {
            GameEvents.onChangePlayerHeight += UseSittingHeight;
            
           

        }
    }

    private void OnDisable()
    {
        if (GameEvents.current == null)
        {
            Debug.LogWarning("GameEvent is not yet created");
        }
        else
        {

            GameEvents.onChangePlayerHeight -= UseSittingHeight;
          
        }
    }

    private void UseSittingHeight(bool value)
    {
        if (value)
        {
            
            if (useLocalPotisiton)
            {
                this.transform.localPosition = SittingHeight;
            }
            else
            {
                this.transform.position = SittingHeight;
            }
        }
        else
        {
            if (useLocalPotisiton)
            {
                this.transform.localPosition = StandingHeight;
            }
            else
            {
                this.transform.position = StandingHeight;
                
            }
        }
    }

}
