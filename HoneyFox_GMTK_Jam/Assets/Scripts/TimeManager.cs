using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    public enum TimeSet {Current, Day, Night}

    public TimeSet timeSet;
    public TMP_Text timeText;
    public float gameTime;

    void Start()
    {
        gameTime = 360f;
    }

    private void FixedUpdate()
    {
        if ((int)gameTime < 840f){
            gameTime += Time.deltaTime;
        }
        if ((int)gameTime % 10 == 0)
        {
            int minutes = (int)gameTime % 60;

            int hour = (int)gameTime / 60;
            if (gameTime < 720f)
            {
                timeText.text = hour + ":" + minutes.ToString("00") + "PM";
            }
            else
            {
                timeText.text = hour + ":" + minutes.ToString("00") + "AM";
            }
        }
    } 
}
