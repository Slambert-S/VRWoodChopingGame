using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class demoDavidScript : MonoBehaviour
{
    public Light lightSource;
    public GameObject ground;
    // Start is called before the first frame update
    void Start()
    {
       // this.GetComponent<Renderer>().materials.SetValue()
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Vector3.Distance(this.gameObject.transform.position , ground.gameObject.transform.position) > 10)
        {
            lightSource.intensity = 0.1f;
        }
        else
        {
            lightSource.intensity = 10;
        }
    }
}
