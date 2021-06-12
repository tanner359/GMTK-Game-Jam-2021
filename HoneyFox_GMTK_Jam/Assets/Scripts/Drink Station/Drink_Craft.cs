using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drink_Craft : MonoBehaviour
{
    // Core Components
    Collider2D collider;

    // Requirements List
    bool glass_Placed;

    // Keep track of ingredients added to the drink
    List<Ingredient> current_Ingredients;

    private void Start()
    {
        collider = GetComponent<BoxCollider2D>();
    }

    private void OnMouseUp()
    {
        Vector2 top_Left = collider.bounds.center + new Vector3(-collider.bounds.size.x, collider.bounds.size.y) * 0.5f;
        Vector2 bottom_Right = collider.bounds.center + new Vector3(collider.bounds.size.x, -collider.bounds.size.y) * 0.5f;

        Collider2D[] marked_Cols = Physics2D.OverlapAreaAll(top_Left, bottom_Right);
    }
}
