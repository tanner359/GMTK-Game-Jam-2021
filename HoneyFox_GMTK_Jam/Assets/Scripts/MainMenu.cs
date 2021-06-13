using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public Canvas options_Menu;

    public void Start_Game()
    {
        // Start Game Sequence
    }

    public void Options()
    {
        // Display Options Menu
        GetComponent<Canvas>().enabled = false;
        options_Menu.enabled = true;
    }

    public void Exit_Game()
    {
        Application.Quit();
    }
}
