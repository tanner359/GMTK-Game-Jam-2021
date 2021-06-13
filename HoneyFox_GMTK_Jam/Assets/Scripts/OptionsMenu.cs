using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    public Canvas main_Menu;

    public void Back_To_Main()
    {
        GetComponent<Canvas>().enabled = false;
        main_Menu.enabled = true;
    }

    public void Fullscreen_Toggle(bool option)
    {
        Screen.fullScreen = option;
    }
}
