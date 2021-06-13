using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TicketRack : MonoBehaviour
{
    public static TicketRack instance;

    public GameObject ticketPrefab;
    public Transform tickets;
    public GameObject ingredient;

    public int orderNum = 0;

    private void Awake()
    {
        instance = this;
    }

    public void CreateTicket(Drink drink)
    {
        GameObject ticket = Instantiate(ticketPrefab, tickets);

        ticket.GetComponent<Ticket>().drink = drink;

        ticket.transform.Find("OrderNumber").GetComponent<TMP_Text>().text = "Order #" + orderNum + 1;
        orderNum++;
        ticket.transform.Find("DrinkName").GetComponent<TMP_Text>().text = drink.drink_Name;

        for(int i = 0; i < drink.recipe.ingredients.Count; i++)
        {
            Instantiate(ingredient, ticket.transform.Find("Ingredients").transform).GetComponent<TMP_Text>().text = "- " + drink.recipe.ingredients[i].name;         
        }
    }

    public void RemoveTicket(Drink drink)
    {
        Bartender.instance.drinkOrders.Remove(drink);
        for(int i = 0; i < tickets.childCount; i++)
        {
            if(tickets.GetChild(i).GetComponent<Ticket>().drink == drink)
            {
                Destroy(tickets.GetChild(i).gameObject);
                i = 9999;
            }
        }
    }
}
