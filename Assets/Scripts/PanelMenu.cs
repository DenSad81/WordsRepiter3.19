using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelMenu : MonoBehaviour
{




    public void OpenPanel()
    {
        gameObject.SetActive(true);
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();// выход
    }


}
