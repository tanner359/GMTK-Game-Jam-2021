using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drink_Craft : MonoBehaviour
{
    // Core Components
    Collider2D collider;

    // Requirements List
    bool check_Item;
    bool in_Border;
    public bool glass_Placed;

    // Keep track of ingredients added to the drink
    List<Ingredient> current_Ingredients;

    List<GameObject> used_Objects;

    public Recipe crafted_Recipe;

    private void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        current_Ingredients = new List<Ingredient>();
        used_Objects = new List<GameObject>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
            check_Item = false;
        else if (Input.GetMouseButtonUp(0) && in_Border)
            check_Item = true;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<Click_and_Draggable>().ingredient != null)
        {
            if (!other.GetComponent<Click_and_Draggable>().ingredient.on_Platform)
                in_Border = true;

            if (check_Item && !other.isTrigger)
            {
                check_Item = false;
                in_Border = false;

                Debug.Log("Platform called the mouse up");

                Ingredient marked_Ingredient = other.GetComponent<Click_and_Draggable>().ingredient;

                if (!marked_Ingredient.on_Platform)
                {
                    Debug.Log("Ingredient has not yet been on platform");

                    if (!other.gameObject.CompareTag("Temp"))
                        used_Objects.Add(other.gameObject);

                    if (marked_Ingredient.is_Glass && !glass_Placed)
                    {
                        Debug.Log("Glass placed on platform");
                        Add_Item_To_Recipe(marked_Ingredient);
                        marked_Ingredient.on_Platform = true;
                        glass_Placed = true;
                        other.isTrigger = true;
                    }
                    else if (!marked_Ingredient.is_Glass && glass_Placed)
                    {
                        Debug.Log("Ingredient placed in glass");

                        Create_Ingredient(marked_Ingredient);

                        other.gameObject.GetComponent<SpriteRenderer>().enabled = false;

                        if (other.gameObject.CompareTag("Temp"))
                            Destroy(other.gameObject);
                        else
                        {
                            GetComponent<AudioSource>().Play();
                            
                            GameObject[] glasses = GameObject.FindGameObjectsWithTag("Glass");

                            foreach(GameObject glass in glasses)
                            {
                                if (glass.GetComponent<Click_and_Draggable>().ingredient.on_Platform)
                                {
                                    glass.GetComponent<Animator>().Play("Fill");
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    public void Create_Ingredient(Ingredient marked_Ingredient)
    {
        Ingredient new_Ingredient = ScriptableObject.CreateInstance("Ingredient") as Ingredient;
        new_Ingredient.Init(marked_Ingredient.ingredient_Name, marked_Ingredient.is_Glass,
                                                    marked_Ingredient.on_Platform, marked_Ingredient.sprite,
                                                    marked_Ingredient.ingredient_State);
        
        Add_Item_To_Recipe(new_Ingredient);
    }

    public void Add_Item_To_Recipe(Ingredient ing)
    {
        current_Ingredients.Add(ing);
    }

    public void Craft_Drink()
    {
        crafted_Recipe = ScriptableObject.CreateInstance("Recipe") as Recipe;
        Bartender.instance.currentDrink = crafted_Recipe;
        crafted_Recipe.init(current_Ingredients);

        foreach (Ingredient ingredient in current_Ingredients)
        {
            ingredient.on_Platform = false;
        }

        foreach (GameObject thing in used_Objects)
        {
            thing.GetComponent<SpriteRenderer>().enabled = true;

            if (thing.CompareTag("Glass"))
            {
                thing.GetComponent<Click_and_Draggable>().ingredient.on_Platform = false;
                thing.GetComponent<Click_and_Draggable>().Full_Reset();
            }
        }

        glass_Placed = false;

        used_Objects.Clear();
        current_Ingredients.Clear();
    }
}
