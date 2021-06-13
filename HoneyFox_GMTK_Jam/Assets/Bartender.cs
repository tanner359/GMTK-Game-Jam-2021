using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bartender : MonoBehaviour
{
    public static Bartender instance;

    public List<Drink> drinkOrders = new List<Drink>();
    public Transform tickets;

    public Recipe currentDrink;

    public int currentScore = 0;
    public TMP_Text scoreText;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        currentScore = 0;
    }

    public void AddOrder(Drink drink)
    {
        drinkOrders.Add(drink);
    }

    public void AddScore(int amount)
    {
        currentScore += amount;
        scoreText.text = "Earnings: " + "$" + currentScore.ToString();
    }

}
