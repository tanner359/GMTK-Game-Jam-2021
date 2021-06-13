using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Canvas main_Menu;
    public AudioSource background_Music;

    public void Back_To_Main()
    {
        GetComponent<Canvas>().enabled = false;
        main_Menu.enabled = true;
    }

    public void Fullscreen_Toggle()
    {
        Screen.fullScreen = GameObject.FindGameObjectWithTag("FS").GetComponent<Toggle>().isOn;
    }

    public void Music_Toggle()
    {
        background_Music.mute = GameObject.FindGameObjectWithTag("Mute").GetComponent<Toggle>().isOn;
    }
}
