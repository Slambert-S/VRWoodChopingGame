using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;

public class UIMultyTextTool : MonoBehaviour
{
    [SerializeField]
    private GameObject[] pagesOfText;
    private int activePageIndex = 0;
    public int pageToBeOpenFirst = 0;
    public bool displayHideOnFirstPage;

    [Header("Button Used")]
    [SerializeField]
    private Button nextButton;
    [SerializeField]
    private Button previousButton;
    [SerializeField]
    private Button hideButtonEnd;
    [SerializeField]
    private Button hideButtonStart;



    [Header("Edit mode control")]
    public int EditorActivePage = 0;

    // Start is called before the first frame update
    void Start()
    {
        activePageIndex = pageToBeOpenFirst;
       
        for (int i = 0; i < pagesOfText.Length; i++)
        {
            HidePage(i);
        }
        ShowPage(pageToBeOpenFirst);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangePage(int direction)
    {

        // testing to not break 
        // Set real test in the future
        if(activePageIndex < 0)
        {
            Debug.Log("wrong value ");
            return;
        }
        HidePage(activePageIndex);

        if (direction <= -1)
        {
          //This can currently overflow and underflow the value

            // Add a check for underflow
            if(activePageIndex != 0)
            {
                activePageIndex--;
            }
            else
            {
                Debug.Log("Debug : tried to go under the array size");
            }
           
        }
        else if( direction >= 1)
        {
            //Check if the active page index is not the last one in the array.
            if (activePageIndex < pagesOfText.Length -1 )
            {
                /* next
                 * active page index = 0  (page 1)| pagesOfText.Length = 3
                 * active page index ++ 
                 * show (page 2)
                 * 
                 * next
                 * active page index = 1 (page 2), 1 < 3 -> good
                 * active page index ++ -> 2
                 * show (page 3)
                 * 
                 * next (we dont want to be able to click)
                 * active page index  = 2 (page 3), 2 <
                 * 
                 * 
                 * */
                activePageIndex++;
            }
            else
            {
                Debug.Log("Debug : tried to go over the array size");
            }
            
        }
        else
        {
            Debug.LogWarning("Wrong value entered in the [Direction] value for function ChangePage");
        }
        // if they would have been and overflow or underflow error the previous page would be re-activate
        ShowPage(activePageIndex);
    }

    public void UpdatePageThatIsActive()
    {
        if(EditorActivePage < 0 || EditorActivePage >= pagesOfText.Length)
        {
            Debug.Log(" DEBUG : Wrong value in [EditorActivePage]");
        }
        else
        {
            for (int i = 0; i < pagesOfText.Length; i++)
            {
                HidePage(i);
            }
            ShowPage(EditorActivePage);
        }
    }

    private void HidePage(int targetedPage)
    {
        // take page in array , hid it 
        pagesOfText[targetedPage].gameObject.SetActive(false);
    }

    private void ShowPage(int targetedPage)
    {
        pagesOfText[targetedPage].gameObject.SetActive(true);

        if(targetedPage < pagesOfText.Length - 1)
        {
            if (hideButtonEnd)
                HideButton(hideButtonEnd);
            if(hideButtonStart)
                HideButton(hideButtonStart);
            ShowButton(nextButton);
            ShowButton(previousButton);
        }

        if(targetedPage == 0)
        {
            HideButton(previousButton);
            if (displayHideOnFirstPage)
            {
                if(hideButtonStart)
                    ShowButton(hideButtonStart);
            }

            //ShowButton(nextButton);
        }
        else if (targetedPage == pagesOfText.Length-1)
        {
            //if the last page is display
            HideButton(nextButton);
            if(hideButtonEnd)
                ShowButton(hideButtonEnd);
            ShowButton(previousButton);
        }
    }

    private void HideButton(Button button)
    {
        button.gameObject.SetActive(false);
    }

    private void ShowButton(Button button)
    {
        button.gameObject.SetActive(true);
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(UIMultyTextTool))]
public class updateActiveText : Editor
{
    // private SerializedProperty
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.LabelField("Future button");

        UIMultyTextTool textTool = (UIMultyTextTool)target;

        /*  if (GUILayout.Button("Generate Cube position", GUILayout.Width(120f)))
          {
              //call function

              map.doSomething();
          }*/

        if (GUILayout.Button("Show text", GUILayout.Width(120f)))
        {
            //call function

            textTool.UpdatePageThatIsActive();
        }

       
    }
}
#endif