using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class to_sizeConverter : MonoBehaviour
{
  
    public void ConvertToM(Vector3 feet, Vector3 inch)
    {
        Vector3 meter = new Vector3(0.0f,0.0f,0.0f);
        meter.x += FeetToM(feet.x);
        meter.x += InchToM(inch.x);

        meter.y += FeetToM(feet.y);
        meter.y += InchToM(inch.y);

        meter.z += FeetToM(feet.z);
        meter.z += InchToM(inch.z);


        this.gameObject.transform.localScale = meter;
    }

    public float FeetToM(float feet)
    {
        float meter = feet / 3.2808f;
        return meter;
    }

    public float InchToM(float inch)
    {
        float meter = inch / 39.370f;
        return meter;
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(to_sizeConverter))]
public class convertmeasurment : Editor
{
    // private SerializedProperty
    Vector3 feet = new Vector3(0.0f,0.0f,0.0f);
    Vector3 inch = new Vector3(0.0f, 0.0f, 0.0f);
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        //EditorGUILayout.LabelField("Future button");
        to_sizeConverter scalingTool = (to_sizeConverter)target;

        EditorGUILayout.BeginHorizontal();
        feet = EditorGUILayout.Vector3Field("Feet:", feet);
        /*if (GUILayout.Button("Capture Position"))
            obj1 = Selection.activeTransform.position;*/
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        inch = EditorGUILayout.Vector3Field("Inch:", inch);
        /*if (GUILayout.Button("Capture Position"))
            obj1 = Selection.activeTransform.position;*/
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Convert ans scale", GUILayout.Width(120f)))
        {
            //call function
            scalingTool.ConvertToM(feet, inch);
            //textTool.UpdatePageThatIsActive();
        }


    }
}
#endif
