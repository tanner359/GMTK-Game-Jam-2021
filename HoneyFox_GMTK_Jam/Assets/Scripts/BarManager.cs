using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarManager : MonoBehaviour
{
    public static BarManager instance;

    public Transform seats;
    public Transform door;
    public Transform exit;

    private void Awake()
    {
        instance = this;
    }

    public Seat FindSeat()
    {
        for(int i = 0; i < seats.childCount; i++)
        {
            if (seats.GetChild(i).GetComponent<Seat>().isAvailable)
            {
                return seats.GetChild(i).GetComponent<Seat>();
            }
        }
        return null;
    }
}
