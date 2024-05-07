using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Colors
{
    
    public List<Color> colors;
}

[System.Serializable]
public class ColorList
{
   
    public List<Colors> colorList;
}
public class listTesting : MonoBehaviour
{
    // Start is called before the first frame update

    public ColorList listOfColor = new ColorList();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
