using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsMenu : MonoBehaviour
{
    public Canvas main_Menu;
    public AudioSource background_Music;

    public TMP_FontAsset og_Font;
    public TMP_FontAsset dys_Font;

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

    public void Font_Toggle()
    {
        TextMeshProUGUI[] fonts = Object.FindObjectsOfType<TextMeshProUGUI>();

        if (GameObject.FindGameObjectWithTag("Dyslexic").GetComponent<Toggle>().isOn)
        {
            foreach (TextMeshProUGUI text in fonts)
            {
                text.font = dys_Font;
            }
        }
        else
        {
            foreach (TextMeshProUGUI text in fonts)
            {
                text.font = og_Font;
            }
        }
    }

    public void Resolution_Change()
    {
        if (GameObject.FindGameObjectWithTag("Resolution").GetComponent<TMP_Dropdown>().value == 0f)
        {
            Screen.SetResolution(1920, 1080, Screen.fullScreen);
        }
        else
        {
            Screen.SetResolution(1280, 720, Screen.fullScreen);
        }
    }
}
