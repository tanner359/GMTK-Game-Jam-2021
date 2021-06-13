using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public Canvas options_Menu;
    public GameObject game_Manager;

    public void Start_Game()
    {
        // Start Game Sequence
        game_Manager.GetComponent<CustomerSystem>().enabled = true;
        game_Manager.GetComponent<TimeManager>().enabled = true;
        GetComponent<Canvas>().enabled = false;
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
