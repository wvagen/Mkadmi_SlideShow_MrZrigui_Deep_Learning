using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MS_SlideShow_Canvas : MonoBehaviour
{

    private List<MS_SlideShow_Page> pages;
    private MS_SlideShow_Page currentPage;

    int currentPageIndex = 0;

    void Start()
    {
        Init();
        Set_Pages();
        Pages_Behaviour_Config();
    }

    void Init()
    {
        pages = new List<MS_SlideShow_Page>();
        currentPageIndex = 0;
    }

    void Set_Pages()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<MS_SlideShow_Page>() != null)
            {
                pages.Add(transform.GetChild(i).GetComponent<MS_SlideShow_Page>());
            }
        }
    }

    void Pages_Behaviour_Config()
    {
        for (int i = 0; i < pages.Count; i++)
        {
            pages[i].Set_Active(false);
            pages[i].Set_Me(i + 1);
        }
        if (pages.Count > 0)
        {
            currentPage = pages[0];
            currentPage.Set_Active(true);
        }
    }

    void Update()
    {
        Page_Switch();
    }

    void Page_Switch()
    {
        if ((currentPageIndex < pages.Count - 1) && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.RightArrow)))
        {
            currentPage.Set_Active(false);
            currentPageIndex++;
            currentPage = pages[currentPageIndex];
            currentPage.Set_Active(true);
        }
        
        if ((currentPageIndex > 0) && (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.LeftArrow)))
        {
            currentPage.Set_Active(false);
            currentPageIndex--;
            currentPage = pages[currentPageIndex];
            currentPage.Set_Active(true);
        }
    }
}
