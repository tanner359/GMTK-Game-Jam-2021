using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bartender : MonoBehaviour
{
    public static Bartender instance;

    public List<Drink> drinkOrders = new List<Drink>();

    public Recipe currentDrink;

    public int currentScore = 0;

    private void Awake()
    {
        instance = this;
    }

    public void AddOrder(Drink drink)
    {
        drinkOrders.Add(drink);
    }

    public void AddScore(int amount)
    {
        currentScore += amount;
    }

}
