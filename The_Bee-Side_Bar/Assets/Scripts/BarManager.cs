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
        List<Seat> availableSeats = new List<Seat>();

        for(int i = 0; i < seats.childCount; i++)
        {
            if (seats.GetChild(i).GetComponent<Seat>().isAvailable)
            {
                availableSeats.Add(seats.GetChild(i).GetComponent<Seat>());
            }
        }
        if(availableSeats.Count > 0)
        {
            return availableSeats[Random.Range(0, availableSeats.Count)];
        }
        return null;
    }
}
