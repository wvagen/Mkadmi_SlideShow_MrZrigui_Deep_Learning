using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MS_SlideShow_Page : MonoBehaviour
{
    public Text pageNumberTxt;

    public void Set_Me(int myPageIndex)
    {
        if (pageNumberTxt != null)
        {
            pageNumberTxt.text = myPageIndex.ToString();
        }
    }

    public void Set_Active(bool isShowing)
    {
        gameObject.SetActive(isShowing);
    }

}
