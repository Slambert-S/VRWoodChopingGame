using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAxeCrackVisual : MonoBehaviour
{
    [SerializeField]
    private List<float> crackValue = new List<float>();
    private Renderer objectRenderer;
    [SerializeField]
    private Color lastLifeColor;
    private Color originalColor;
    // Start is called before the first frame update
    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        Debug.Log(crackValue.Capacity);
      
        originalColor = objectRenderer.material.GetColor("_CrackColor");
        
        
    }

    public void OnEnable()
    {
        GameEvents.onGameReset += ResetCrack;
    }

    public void OnDisable()
    {
     
        GameEvents.onGameReset -= ResetCrack;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseCrackVisual()
    {
        int life = StatTraking.current.getNumberOfLifeLost();
        if (objectRenderer != null)
        {
            if(life + 1 < crackValue.Capacity  )
            {
                //when i have 3 element return 3
                /*
                 * life 0 +1 = Index [0|X|2]  
                 *  1 +1 = 2 = Index [0|1|X]
                 *  2 +2 = 3 = Index [0|1|2] X out of range
                 * 
                 * 
                 */
                objectRenderer.material.SetFloat("_CrackProgress", crackValue[life+1]);

                // Check if life remaining is 2 because this check is done after the player hit the log but before they lose a life.
                // Right here 2 mean the player will be on its last life after.
                if(StatTraking.current.GetLifeRemaining() == 2)
                {
                    objectRenderer.material.SetColor("_CrackColor", lastLifeColor);
                }
            }
        }
        else
        {
            Debug.LogWarning("this object does not have a renderer");
        }
        
    }

    private void ResetCrack()
    {

        if (objectRenderer != null)
        {
           
           objectRenderer.material.SetFloat("_CrackProgress", crackValue[0]);
           objectRenderer.material.SetColor("_CrackColor", originalColor );
        }
        else
        {
            Debug.LogWarning("this object does not have a renderer");
        }
    }
}
