using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ingredient", menuName = "Custom Objects/Ingredient")]
public class Ingredient : ScriptableObject
{
    public string ingredient_Name;
    public Sprite sprite;
    public enum State {Raw, Blended, Juiced};
}
