using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ingredient", menuName = "Custom Objects/Ingredient")]
public class Ingredient : ScriptableObject
{
    public string ingredient_Name; // Name of ingredient
    public bool is_Glass; // Is this object a glass?
    public bool on_Platform; // Is this object already on the drink platform?
    public Sprite sprite; // The sprite of the ingredient
    public enum State {Raw, Blended, Juiced}; // The state the object is currently in (Raw = default state)
    public State ingredient_State;
    public Color juice_Color;

    public void Init(string ing_Name, bool glass, bool plat, Sprite spr, State state)
    {
        ingredient_Name = ing_Name;
        is_Glass = glass;
        on_Platform = plat;
        sprite = spr;
        ingredient_State = state;
    }
}
