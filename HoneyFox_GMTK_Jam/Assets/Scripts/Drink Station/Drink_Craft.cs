using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drink_Craft : MonoBehaviour
{
    // Core Components
    Collider2D collider;

    // Requirements List
    public bool glass_Placed;

    // Keep track of ingredients added to the drink
    List<Ingredient> current_Ingredients;

    private void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        current_Ingredients = new List<Ingredient>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("Platform called the mouse up");

            Ingredient marked_Ingredient = other.GetComponent<Ingredient>();

            if (!marked_Ingredient.on_Platform)
            {
                if (marked_Ingredient.is_Glass && !glass_Placed)
                {
                    current_Ingredients.Add(marked_Ingredient);
                    marked_Ingredient.on_Platform = true;
                    glass_Placed = true;
                }
                else if (!marked_Ingredient.is_Glass && glass_Placed)
                {
                    current_Ingredients.Add(marked_Ingredient);
                    // Trigger an adding animation here
                    Destroy(other.transform.gameObject);
                }
            }
        }
    }

    public void Craft_Drink()
    {
        Recipe drink_Recipe = new Recipe();

        drink_Recipe.ingredients = current_Ingredients;
        current_Ingredients.Clear();
    }
}
