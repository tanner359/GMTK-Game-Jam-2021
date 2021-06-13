using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blender : MonoBehaviour
{
    private Drink_Craft craft_Core;

    bool in_Border;
    bool check_Item;
    bool blend_Active;

    private void Start()
    {
        craft_Core = GameObject.FindGameObjectWithTag("Drink Platform").GetComponent<Drink_Craft>();
    }

    private void Update()
    {
        if (blend_Active && in_Border)
            check_Item = true;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<Click_and_Draggable>().ingredient != null)
        {
            Debug.Log("Object is hovering over blender");

            Ingredient curr_Ingredient = other.GetComponent<Click_and_Draggable>().ingredient;

            if (!curr_Ingredient.on_Platform)
                    in_Border = true;

            if (check_Item && !other.isTrigger && craft_Core.glass_Placed)
            {
                Debug.Log("Blending item...");

                check_Item = false;
                in_Border = false;

                curr_Ingredient.ingredient_State = Ingredient.State.Blended;

                craft_Core.Create_Ingredient(curr_Ingredient);

                Destroy(other.gameObject);
            }

            //blend_Active = false;
        }
    }

    public void Activate_Blender()
    {
        if (!blend_Active)
        blend_Active = true;
    }
}
