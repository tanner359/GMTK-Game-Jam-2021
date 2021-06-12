using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Drink", menuName = "Custom Objects/Drink")]
public class Drink : ScriptableObject
{
    public string drink_Name;
    public string description;
    public Recipe recipe;
}
