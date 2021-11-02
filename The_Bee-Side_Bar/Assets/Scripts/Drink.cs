using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Drink", menuName = "Custom Objects/Drink")]
public class Drink : ScriptableObject
{
    public string drink_Name; // The name of the drink
    public string description; // The description of the drink
    public Recipe recipe; // The recipe reference for the customer to check
}
