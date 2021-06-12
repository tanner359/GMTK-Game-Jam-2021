using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "Custom Objects/Recipe")]
public class Recipe : ScriptableObject
{
    public List<Ingredient> ingredients; // The ingredients in order to create this recipe

    public void init(List<Ingredient> ingred)
    {
        ingredients = new List<Ingredient>();

        foreach (Ingredient i in ingred)
        {
            ingredients.Add(i);
        }
    }
}
