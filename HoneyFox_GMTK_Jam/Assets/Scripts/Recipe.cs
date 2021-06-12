using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "Custom Objects/Recipe")]
public class Recipe : ScriptableObject
{
    List<Ingredient> ingredients;
}
