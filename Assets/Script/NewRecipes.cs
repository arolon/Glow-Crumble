using System.Collections.Generic;
using UnityEngine;

public class NewRecipes : MonoBehaviour
{
    public InventorySystem inventory;  
    private List<string> discoveredRecipes = new List<string>();  

    void Start()
    {
        if (inventory == null)
            inventory = GetComponent<InventorySystem>();
    }

    
    public void CheckForNewRecipes()
    {
        if (inventory.HasItem("Flour") && inventory.HasItem("Egg") && !discoveredRecipes.Contains("Cake"))
        {
            DiscoverRecipe("Cake");
        }
        if (inventory.HasItem("Strawberry") && inventory.HasItem("Banana") && !discoveredRecipes.Contains("Smoothie"))
        {
            DiscoverRecipe("Smoothie");
        }
        // Add more recipes here...
    }

    void DiscoverRecipe(string recipeName)
    {
        discoveredRecipes.Add(recipeName);
        Debug.Log("New Recipe Discovered: " + recipeName);
    }

    public bool IsRecipeDiscovered(string recipeName)
    {
        return discoveredRecipes.Contains(recipeName);
    }
}
