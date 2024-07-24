using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toggleObjectAtGameOver : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isObjectActive = false;
    public GameObject ObjectToControll;

    public float _deadTime = 2.0f;

    public void Start()
    {
        isObjectActive = !isObjectActive;
        toogleButton();
        
    }
    public void OnEnable()
    {

        GameEvents.current.onGameOver += toogleButton; 
        GameEvents.current.onGameReset += toogleButton;
    }

    public void OnDisable()
    {
        GameEvents.current.onGameOver -= toogleButton;
        GameEvents.current.onGameReset -= toogleButton;
    }
    // Update is called once per frame
    
    public void toogleButton()
    {
        isObjectActive = !isObjectActive;
        if (isObjectActive)
        {
            //start coroutine
            StartCoroutine(WaitForDeadTime());
        }
        else
        {
            if(ObjectToControll.transform.childCount == 0)
            {
                ObjectToControll.GetComponent<MeshRenderer>().enabled = false;
            }
            else
            {
                foreach (Transform child in ObjectToControll.transform)
                {
                    child.gameObject.GetComponent<MeshRenderer>().enabled = false;
                    BoxCollider oBC = child.gameObject.GetComponent<BoxCollider>();
                    if(oBC != null)
                    {
                        oBC.enabled = false;
                    }
                }
            }
            
            
        }
       
        //ObjectToControll.SetActive(isObjectActive);
    }

    IEnumerator WaitForDeadTime()
    {
       
        yield return new WaitForSeconds(_deadTime);
        if (ObjectToControll.transform.childCount == 0)
        {
            ObjectToControll.GetComponent<MeshRenderer>().enabled = true;
        }
        else
        {
            foreach (Transform child in ObjectToControll.transform)
            {
                child.gameObject.GetComponent<MeshRenderer>().enabled = true;
                BoxCollider oBC = child.gameObject.GetComponent<BoxCollider>();
                if (oBC != null)
                {
                    oBC.enabled = true;
                }

            }
        }
        
    }
}
