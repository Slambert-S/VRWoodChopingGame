using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlicedObjectLifetime : MonoBehaviour
{
    public float lifetime = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartLifeTime()
    {
        StartCoroutine(TimeBeforeDeath(lifetime));
    }


    IEnumerator TimeBeforeDeath(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(this.gameObject);
    }
}
